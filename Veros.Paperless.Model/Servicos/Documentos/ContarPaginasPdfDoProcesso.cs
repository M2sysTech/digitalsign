namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System;
    using System.IO;
    using System.Linq;
    using Entidades;
    using Framework;
    using Framework.IO;
    using Pdf;
    using Repositorios;
    using Storages;

    public class ContarPaginasPdfDoProcesso : IContarPaginasPdfDoProcesso
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPdfServico pdfServico;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IFileSystem fileSystem;
        private readonly IBaixarArquivoAmazonS3 baixarArquivoAmazonS3;

        public ContarPaginasPdfDoProcesso(
            IDocumentoRepositorio documentoRepositorio, 
            IPdfServico pdfServico, IPaginaRepositorio paginaRepositorio, 
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IFileSystem fileSystem,
            IBaixarArquivoAmazonS3 baixarArquivoAmazonS3)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.pdfServico = pdfServico;
            this.paginaRepositorio = paginaRepositorio;
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.fileSystem = fileSystem;
            this.baixarArquivoAmazonS3 = baixarArquivoAmazonS3;
        }

        public int Executar(int processoId)
        {
            var documentos = this.documentoRepositorio.ObterPdfsPorProcesso(processoId);

            var totalPaginasDoProcesso = 0;
            
            Log.Application.InfoFormat("Contando paginas do processo #{0}", processoId);

            foreach (var documento in documentos.Where(x => x.TipoDocumento.Id != TipoDocumento.CodigoFolhaDeRosto && x.TipoDocumento.Id != TipoDocumento.CodigoTermoAutuacaoDossie))
            {
                //// se o numero já estiver calculado (quem faz isso é o Directory), não precisa baixar o PDF daquele item documental
                if (documento.QuantidadeDePaginas > 1)
                {
                    totalPaginasDoProcesso += documento.QuantidadeDePaginas;
                    continue;
                }

                var pagina = this.paginaRepositorio.ObterPdfDocumento(documento.Id);

                if (pagina == null)
                {
                    continue;
                }

                string pdf = string.Empty;

                try
                {
                    if (pagina.CloudOk)
                    {
                        var fileName = string.Format("{0}.{1}", pagina.Id.ToString("000000000"), pagina.TipoArquivo);
                        pdf = Path.Combine(Aplicacao.Caminho, "Images", fileName);

                        this.baixarArquivoAmazonS3.BaixarArquivo(pagina, pdf);
                    }
                    else
                    {
                        pdf = this.baixaArquivoFileTransferServico.BaixarArquivo(pagina.Id, pagina.TipoArquivo);
                    }

                    var totalPaginasDoDocumento = this.pdfServico.ContarPaginas(pdf);

                    if (totalPaginasDoDocumento <= 0)
                    {
                        Log.Application.ErrorFormat("Não conseguiu contar paginas do documento PDF :: {0}", documento.Id);
                        continue;
                    }

                    totalPaginasDoProcesso += totalPaginasDoDocumento;

                    this.documentoRepositorio.AtualizarQuantidadeDePaginas(documento.Id, totalPaginasDoDocumento);
                }
                catch (System.Exception exception)
                {
                    Log.Application.Error(exception);
                    throw;
                }
                finally
                {
                    try
                    {
                        this.fileSystem.DeleteFile(pdf);
                    }
                    catch (System.Exception)
                    {
                        Log.Application.Warn("Erro ao tentar excluir pdf >> " + pdf);
                    }
                }
            }

            return totalPaginasDoProcesso;
        }
    }
}