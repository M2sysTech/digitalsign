namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.IO;
    using System.Linq;
    using Entidades;
    using Framework;
    using Framework.IO;
    using Importacao;
    using Pdf;
    using Repositorios;

    public class CriaDocumentoPdfServico : ICriaDocumentoPdfServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ICriaDocumentoCapturaServico criaDocumentoCapturaServico;
        private readonly IPaginaFabrica paginaFabrica;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IPdfServico pdfServico;
        private readonly IPostaArquivoAmazonS3 postaArquivoAmazonS3;

        public CriaDocumentoPdfServico(
            IDocumentoRepositorio documentoRepositorio, 
            ICriaDocumentoCapturaServico criaDocumentoCapturaServico, 
            IPaginaFabrica paginaFabrica, 
            IPaginaRepositorio paginaRepositorio, 
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico, 
            IPdfServico pdfServico, 
            IPostaArquivoAmazonS3 postaArquivoAmazonS3)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.criaDocumentoCapturaServico = criaDocumentoCapturaServico;
            this.paginaFabrica = paginaFabrica;
            this.paginaRepositorio = paginaRepositorio;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.pdfServico = pdfServico;
            this.postaArquivoAmazonS3 = postaArquivoAmazonS3;
        }

        public Documento CriarNovoDocumentoPdf(Processo processo, Documento documento, string pathArquivoPdf, int ordem = 1)
        {
            var versaoAtual = 0;
            var outrosPdfsJaVersionados = processo.Documentos.Where(x => x.Cpf == documento.Cpf && x.Virtual && x.DocumentoPaiId == documento.Id);
            var tipoPdfAnterior = -1;

            if (outrosPdfsJaVersionados.Any())
            {
                var pdfMaisRecente = outrosPdfsJaVersionados.OrderByDescending(x => x.Versao).FirstOrDefault();
                versaoAtual = pdfMaisRecente.Versao.ToInt() + 1;
                pdfMaisRecente.AlterarStatusDasPaginas(PaginaStatus.StatusExcluida);
                tipoPdfAnterior = pdfMaisRecente.TipoDocumento.Id;
                //// forçando a barra na exclusão
                pdfMaisRecente.Status = DocumentoStatus.Excluido;
                this.documentoRepositorio.AlterarStatus(pdfMaisRecente.Id, DocumentoStatus.Excluido);
                Log.Application.InfoFormat("Versao anterior {0} ja existe. Nova versao {1}. Pdfmaisrecente={2}", pdfMaisRecente.Id, versaoAtual, pdfMaisRecente.Id);
            }

            var documentoMarcadoNoAjuste = processo.Documentos.Where(x => x.Marca == "M" || x.Marca == "A");

            if (documentoMarcadoNoAjuste.Any())
            {
                if (versaoAtual == 0)
                {
                    versaoAtual = documento.Versao.ToInt() + 1;    
                }

                this.documentoRepositorio.AlterarStatus(documento.Id, DocumentoStatus.Excluido);
                documento.Status = DocumentoStatus.Excluido;
                this.paginaRepositorio.AlterarStatus(documento, 0, PaginaStatus.StatusExcluida);
                Log.Application.InfoFormat("Documento marcado no ajuste {0}. versao {1}", documento.Id, versaoAtual);
            }

            var tipoDocumentoId = this.DefinirTipoDocumento(documento, pathArquivoPdf, tipoPdfAnterior);

            var documentoPdf = this.criaDocumentoCapturaServico
                .CriarForcado(processo.Id, tipoDocumentoId, 1, documento.Cpf, versaoAtual.ToString(), true);

            Log.Application.InfoFormat("Gerando novo pdf do documento original {0}. Novo documento {1}", documento.Id, documentoPdf.Id);

            var totalPaginasPdf = this.pdfServico.ContarPaginas(pathArquivoPdf);

            documentoPdf.DocumentoPaiId = documento.Id;
            documentoPdf.Ordem = ordem;
            documentoPdf.QuantidadeDePaginas = totalPaginasPdf;
            this.documentoRepositorio.Salvar(documentoPdf);

            var multiPaginaPdf = this.paginaFabrica.Criar(documentoPdf, 1, pathArquivoPdf);
            
            if (this.DocumentoJaEstaNoCloudStorage(documento))
            {
                multiPaginaPdf.CloudOk = true;
                multiPaginaPdf.RemovidoFileTransferM2 = true;
            }

            this.paginaRepositorio.Salvar(multiPaginaPdf);

            if (multiPaginaPdf.CloudOk)
            {
                Log.Application.InfoFormat("Postando pagina Pdf no amazon cloud: {0}", pathArquivoPdf);
                this.postaArquivoAmazonS3.PostarPagina(multiPaginaPdf, pathArquivoPdf);
            }
            else
            {
                Log.Application.InfoFormat("Postando pagina Pdf no FILETRANSFER M2SYS: {0}", pathArquivoPdf);
                this.postaArquivoFileTransferServico.PostarPagina(multiPaginaPdf, pathArquivoPdf);
            }

            Log.Application.InfoFormat("Pdf Enviado: {0}", pathArquivoPdf);

            return documentoPdf;
        }

        public int DefinirTipoDocumento(Documento documento, string pathArquivoPdf, int tipoPdfAnterior)
        {
            if (tipoPdfAnterior > 0)
            {
                return tipoPdfAnterior;
            }

            if (pathArquivoPdf.IndexOf("_Tipo_") > 0)
            {
                Log.Application.InfoFormat("Tipo Detectado pelo ABBYY: {0}", pathArquivoPdf);
                var nomeArquivo = Path.GetFileNameWithoutExtension(pathArquivoPdf);
                var tipoDetectadoPeloAbbyy = nomeArquivo.Substring(nomeArquivo.IndexOf("_Tipo_") + 6);
                int saidaId;
                if (int.TryParse(tipoDetectadoPeloAbbyy, out saidaId))
                {
                    Log.Application.InfoFormat("Tipo apropriado : {0}", saidaId);
                    return saidaId;
                }
                
                Log.Application.InfoFormat("Não foi possivel converter em numero o Typedoc detectado pelo ABBYY: {0}", pathArquivoPdf);
            }

            return documento.TipoDocumento.Id;
        }

        private bool DocumentoJaEstaNoCloudStorage(Documento documento)
        {
            var paginas = this.paginaRepositorio.ObterPorDocumentoId(documento.Id);

            if (paginas.Count == 0)
            {
                return true;
            }

            return paginas.Any(x => x.CloudOk);
        }
    }
}
