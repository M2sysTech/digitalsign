namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Framework.IO;
    using Repositorios;
    using Veros.Framework;
    using Veros.Paperless.Model.Entidades;

    public class AdicionaDocumentoAoProcessoServico : IAdicionaDocumentoAoProcessoServico
    {
        private readonly IDocumentoFabrica documentoFabrica;
        private readonly IPaginaFabrica paginaFabrica;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IIndexacaoFabrica indexacaoFabrica;
        private readonly IFileSystem fileSystem;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IValorReconhecidoRepositorio valorReconhecidoRepositorio;
        private readonly IndexacaoDocumento indexacaoDocumento;
        private readonly dynamic imageMagicResolution;

        public AdicionaDocumentoAoProcessoServico(
            IDocumentoFabrica documentoFabrica, 
            IPaginaFabrica paginaFabrica,
            IProcessoRepositorio processoRepositorio,
            IIndexacaoFabrica indexacaoFabrica,
            IFileSystem fileSystem,
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico,
            IIndexacaoRepositorio indexacaoRepositorio,
            IPaginaRepositorio paginaRepositorio,
            IDocumentoRepositorio documentoRepositorio,
            IValorReconhecidoRepositorio valorReconhecidoRepositorio,
            IndexacaoDocumento indexacaoDocumento, 
            dynamic imageMagicResolution)
        {
            this.documentoFabrica = documentoFabrica;
            this.paginaFabrica = paginaFabrica;
            this.processoRepositorio = processoRepositorio;
            this.indexacaoFabrica = indexacaoFabrica;
            this.fileSystem = fileSystem;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.valorReconhecidoRepositorio = valorReconhecidoRepositorio;
            this.indexacaoDocumento = indexacaoDocumento;
            this.imageMagicResolution = imageMagicResolution;
        }

        public void Adicionar(int loteId, IList<ImagemConta> imagens)
        {
            if (imagens == null)
            {
                Log.Application.DebugFormat("Sem imagem para tipo de documento");
                return;
            }

            var primeiraImagem = imagens.First();
            var tipoDocumentoId = primeiraImagem.TipoDocumentoId;

            if (tipoDocumentoId == TipoDocumento.CodigoFoto)
            {
                if (primeiraImagem.FormatoBase64 == "NOK")
                {
                    return;
                }
            }

            var processo = this.processoRepositorio.ObterPorLoteId(loteId);
            var tipoDocumento = new TipoDocumento { Id = tipoDocumentoId };

            var documento = this.documentoFabrica.Criar(processo, tipoDocumento, primeiraImagem.Cpf);
            this.documentoRepositorio.Salvar(documento);

            var cpf = this.indexacaoFabrica.Criar(documento, Campo.ReferenciaDeArquivoCpf, primeiraImagem.Cpf);

            if (cpf != null)
            {
                this.indexacaoRepositorio.Salvar(cpf);
            }

            this.indexacaoDocumento.Executar(documento, primeiraImagem.Cpf);
            
            foreach (var imagem in imagens)
            {
                this.AdicionarPagina(loteId, imagem, documento);
            }
        }

        private void AdicionarPagina(int loteId, ImagemConta imagem, Documento documento)
        {
            var nomeArquivo = string.Format(
                "{0}_{1}_{2}",
                imagem.TipoDocumentoId,
                Guid.NewGuid(),
                imagem.ObterTipoArquivo());

            Directories.CreateIfNotExist(
                Path.Combine(Configuracao.CaminhoDePacotesRecebidos, loteId.ToString(), "extraidos"));
            
            var path = new[]
            {
                Configuracao.CaminhoDePacotesRecebidos, 
                loteId.ToString(),
                "extraidos",
                nomeArquivo
            };

            var caminhoImagem = Path.Combine(path);

            if (imagem.FormatoBase64 == "NOK")
            {
                return;
            }

            this.fileSystem.CreateFileFromBase64(caminhoImagem, imagem.ObterBase64());

            ///// #17888 - verifica se arquivo já está com DPI no header, insere/altera se não estiver. 
            this.DefinirResolucao(caminhoImagem);

            //// TODO: conversao para JPG compactado
            ////var arquivoParaPostar = caminhoImagem;
            ////if (this.DeveConverterParaJpg(caminhoImagem, documento.TipoDocumento))
            ////{
            ////    arquivoParaPostar = this.converterImagem
            ////        .ParaJpeg(imagemDocumento.Caminho, RemotePath.GetExtension(imagemDocumento.Caminho));
            ////}

            var pagina = this.paginaFabrica.Criar(documento, imagem.Face, caminhoImagem);
            this.paginaRepositorio.Salvar(pagina);

            this.InserirValorReconhecido(imagem, pagina);

            documento.Paginas.Add(pagina);

            this.postaArquivoFileTransferServico.PostarPagina(pagina, pagina.CaminhoCompletoDoArquivo);
        }

        private bool DeveConverterParaJpg(string caminho, TipoDocumento tipoDocumento)
        {
            if (tipoDocumento.Id == TipoDocumento.CodigoFichaDeCadastro
                || tipoDocumento.Id == TipoDocumento.CodigoAssinatura)
            {
                return false;
            }

            try
            {
                var tiposParaConverter = new List<string>() { "PNG", "GIF", "JPG", "BMP" };
                var extensao = Path.GetExtension(caminho).ToUpper().Replace(".", string.Empty);
                if (tiposParaConverter.IndexOf(extensao) >= 0)
                {
                    var length = new System.IO.FileInfo(caminho).Length;
                    if (length > 1000000)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception exception)
            {
                Log.Application.Error("Não foi possivel avaliar extensao do arquivo para conversao JPG.", exception);
                return false;
            }
        }

        private void InserirValorReconhecido(ImagemConta imagem, Pagina pagina)
        {
            try
            {
                var valorReconhecido = new ValorReconhecido
                {
                    Pagina = pagina,
                    TemplateName = "fulltext",
                    CampoTemplate = "fulltext",
                    Value = imagem.Ocr
                };

                this.valorReconhecidoRepositorio.Salvar(valorReconhecido);
            }
            catch (Exception exception)
            {
                Log.Application.Error(
                    "Erro ao processar arquivo(s) de OCR.",
                    exception);
            }
        }

        private void DefinirResolucao(string caminhoImagem)
        {
            if (!File.Exists(caminhoImagem))
            {
                return;
            }

            float resolucaoatual = 0;
            try
            {
                resolucaoatual = this.imageMagicResolution.GetHorizontalResolution(caminhoImagem);
            }
            catch (Exception exception)
            {
                Log.Application.DebugFormat("Não foi possivel determinar a resolução da imagem {0}", caminhoImagem);
            }

            if (resolucaoatual < 100)
            {
                Log.Application.DebugFormat("Redefinindo resolução da imagem {0}", caminhoImagem);
                try
                {
                    this.imageMagicResolution.AlterToDesiredDpi(caminhoImagem, caminhoImagem, 100);
                }
                catch (Exception exception)
                {
                    Log.Application.DebugFormat("Não foi possivel alterar a resolução da imagem {0}", caminhoImagem);
                }
            }
        }
    }
}
