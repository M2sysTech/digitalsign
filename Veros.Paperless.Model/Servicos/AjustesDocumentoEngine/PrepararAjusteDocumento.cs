namespace Veros.Paperless.Model.Servicos.AjustesDocumentoEngine
{
    using System;
    using System.IO;
    using Entidades;
    using Framework;
    using Image;
    using Repositorios;

    public class PrepararAjusteDocumento : IPrepararAjusteDocumento
    {
        private readonly IExtrairPdf extrairPdf;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public PrepararAjusteDocumento(
            IExtrairPdf extrairPdf, 
            IDocumentoRepositorio documentoRepositorio, 
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico, 
            IPaginaRepositorio paginaRepositorio,
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.extrairPdf = extrairPdf;
            this.documentoRepositorio = documentoRepositorio;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.paginaRepositorio = paginaRepositorio;
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(Documento documento)
        {
            Log.Application.InfoFormat("Iniciando documento #{0}", documento.Id);

            this.gravaLogDoDocumentoServico.Executar(
                LogDocumento.AcaoDocumentoGenerico, 
                documento.Id, 
                "Inicio Preparacao Ajuste Documento");

            var arquivoPdf = string.Empty;

            try
            {
                arquivoPdf = this.CriarPastaDeTrabalho(documento);
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat(
                    exception,
                    "Erro ao baixar PDF na pasta de trabalho para documento #{0}, no lote #{1}", 
                    documento.Id, 
                    documento.Lote.Id);

                throw;
            }

            try
            {
                this.extrairPdf.ExtrairParaPng(arquivoPdf, arquivoPdf, 300, 61000);
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat(
                    exception,
                    "Erro ao converter PDF em PNG para documento #{0}, no lote #{1}", 
                    documento.Id, 
                    documento.Lote.Id);
                throw;
            }

            var documentoParaAjuste = new Documento
            {
                Lote = documento.Lote,
                Ordem = documento.Ordem,
                DocumentoPaiId = documento.Id,
                TipoDocumento = new TipoDocumento { Id = TipoDocumento.CodigoEmAjuste },
                Status = DocumentoStatus.TelaAjuste,
                Processo = documento.Processo,
                TipoDocumentoOriginal = documento.TipoDocumento,
            };

            this.documentoRepositorio.Salvar(documentoParaAjuste);

            //// cria paginas 
            var totalPags = this.CriarPaginasEmAjuste(documentoParaAjuste, arquivoPdf);

            if (totalPags <= 0)
            {
                var mensagem = string.Format("Nenhum arquivo PNG convertido em Pagina. Aortando criação de Documento: {0}, Lote: {1}", documento.Id, documento.Lote.Id);
                throw new Exception(mensagem);
            }

            //// posta imagens
            this.PostarImagensPng(documentoParaAjuste, arquivoPdf);

            //// muda status do documento original, pra nao fazer de novo no proximo loop
            documento.Status = DocumentoStatus.AjustePreparacaoRealizada;
            this.documentoRepositorio.Salvar(documento);

            //// apaga imagens locais
            this.ExcluirDiretorio(arquivoPdf);

            this.gravaLogDoDocumentoServico.Executar(
                LogDocumento.AcaoDocumentoGenerico, 
                documento.Id, 
                "Fim Preparacao Ajuste Documento");

            Log.Application.InfoFormat("Fim criação PNG para mdoc #{0}", documento.Id);
        }

        private void PostarImagensPng(Documento documentoParaAjuste, string arquivoPdf)
        {
            try
            {
                foreach (var pagina in documentoParaAjuste.Paginas)
                {
                    var pathArquivoPng = Path.Combine(Path.GetDirectoryName(arquivoPdf), pagina.NomeArquivoSemExtensao + ".png");
                    this.postaArquivoFileTransferServico.PostarPagina(pagina, pathArquivoPng);
                    Log.Application.InfoFormat("Pdf Enviado: {0}", pathArquivoPng);
                }
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat(
                    exception,
                    "Erro ao postar no FT o PNG convertido para documento #{0}, no lote #{1}", 
                    documentoParaAjuste.Id,
                    documentoParaAjuste.Lote.Id);
                throw;
            }
        }

        private void ExcluirDiretorio(string arquivoPdf)
        {
            Log.Application.InfoFormat("Apagando pasta {0}", arquivoPdf);

            try
            {
                Directory.Delete(Path.GetDirectoryName(arquivoPdf), true);
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat(
                    exception, 
                    "Não foi possivel apagar pasta {0}", 
                    Path.GetDirectoryName(arquivoPdf));
            }
        }

        private int CriarPaginasEmAjuste(Documento documento, string arquivoPdf)
        {
            var diretorioTrabalho = Path.GetDirectoryName(arquivoPdf);
            var arquivosConvertidos = Directory.GetFiles(diretorioTrabalho, "*.png");

            if (arquivosConvertidos.Length == 0)
            {
                return 0;
            }

            var contador = 1;
            foreach (var arquivoConvertido in arquivosConvertidos)
            {
                var pagina = new Pagina
                {
                    Documento = documento,
                    Lote = documento.Lote,
                    DataCriacao = DateTime.Now,
                    Status = PaginaStatus.StatusTransmissaoOk,
                    TipoArquivo = "PNG",
                    ImagemFront = "S",
                    AgenciaRemetente = "1234",
                    TamanhoImagemFrente = new FileInfo(arquivoConvertido).Length.ToInt(),
                    Ordem = contador,
                    NomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(arquivoConvertido)
                };

                this.paginaRepositorio.Salvar(pagina);
                documento.Paginas.Add(pagina);
                contador++;
            }

            return arquivosConvertidos.Length;
        }

        private string CriarPastaDeTrabalho(Documento documento)
        {
            var ultimoCaminho = string.Empty;

            foreach (var pagina in documento.Paginas)
            {
                var nomeArquivo = string.Format("{0:000000000}.{1}", pagina.Id, pagina.TipoArquivo.ToUpper());
                var pathDestino = Path.Combine(Contexto.PastaAjusteTratamento, string.Format("LOTE_{0}_MDOC_{1}", pagina.Lote.Id, documento.Id), nomeArquivo);

                try
                {
                    this.baixaArquivoFileTransferServico
                        .BaixarArquivoNaPasta(pagina.Id, pagina.TipoArquivo, pathDestino, false);
                }
                catch (Exception exception)
                {
                    Log.Application.ErrorFormat(
                        exception,
                        "Arquivo não foi baixado do FT. Pagina #{0}.",
                        pagina.Id);

                    throw;
                }

                ultimoCaminho = pathDestino;
            }

            return ultimoCaminho;
        }
    }
}