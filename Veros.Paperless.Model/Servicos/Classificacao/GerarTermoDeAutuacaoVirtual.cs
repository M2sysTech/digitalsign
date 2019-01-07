namespace Veros.Paperless.Model.Servicos.Classificacao
{
    using System;
    using System.IO;
    using System.Linq;
    using Entidades;
    using Framework;
    using Importacao;
    using Pdf;
    using Repositorios;

    public class GerarTermoDeAutuacaoVirtual : IGerarTermoDeAutuacaoVirtual
    {
        private readonly IDocumentoFabrica documentoFabrica;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaFabrica paginaFabrica;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IPdfServico pdfServico;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IAssinarPdf assinarPdf;
        private readonly IPostaArquivoAmazonS3 postaArquivoAmazonS3;

        public GerarTermoDeAutuacaoVirtual(
            IDocumentoFabrica documentoFabrica, 
            IProcessoRepositorio processoRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            IPaginaFabrica paginaFabrica, 
            IPaginaRepositorio paginaRepositorio, 
            IPdfServico pdfServico, 
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico, 
            IAssinarPdf assinarPdf, 
            IPostaArquivoAmazonS3 postaArquivoAmazonS3)
        {
            this.documentoFabrica = documentoFabrica;
            this.processoRepositorio = processoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.paginaFabrica = paginaFabrica;
            this.paginaRepositorio = paginaRepositorio;
            this.pdfServico = pdfServico;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.assinarPdf = assinarPdf;
            this.postaArquivoAmazonS3 = postaArquivoAmazonS3;
        }

        public void Executar(int processoId)
        {
            var processo = this.processoRepositorio.ObterDetalheDossieComDocumentos(processoId);

            var termoAutuacao = this.ObterOuCriarTermo(processo); 

            termoAutuacao.Status = DocumentoStatus.TransmissaoOk;

            this.documentoRepositorio.Salvar(termoAutuacao);
            
            var pdf = this.pdfServico.GerarPdfTermoAutuacao(processo);

             var paginas = this.paginaRepositorio.ObterPorDocumentoId(termoAutuacao.Id);

            var pagina = paginas.Any() == false ? 
                this.paginaFabrica.Criar(termoAutuacao, 0, pdf) : 
                paginas.First();

            this.paginaRepositorio.Salvar(pagina);
            
            if (pagina.CloudOk)
            {
                this.postaArquivoAmazonS3.PostarPagina(pagina, pdf);
            }
            else
            {
                this.postaArquivoFileTransferServico.PostarPagina(pagina, pdf);
            }
            
            try
            {
                File.Delete(pdf);
            }
            catch (Exception)
            {
                Log.Application.Warn("Não conseguiu excluir o arquivo temporario do termo de autuacao. não prejudica o processo de geração de termos.");
            }
        }

        private Documento ObterOuCriarTermo(Processo processo)
        {
            var termoEncontrado = this.documentoRepositorio.ObterTermoAutuacao(processo.Id);
            if (termoEncontrado != null)
            {
                ////# 22451 - Ajustar ordem 
                var documentos = processo.Documentos.Where(x => x.Virtual == true 
                    && x.Status != DocumentoStatus.Excluido 
                    && x.TipoDocumento.Id != TipoDocumento.CodigoTermoAutuacaoDossie 
                    && x.TipoDocumento.Id != TipoDocumento.CodigoFolhaDeRosto);

                var quantidadeVirtuais = documentos.Count() != 0 ? documentos.OrderBy(x => x.Ordem).Last().Ordem : 0;
                termoEncontrado.Ordem = quantidadeVirtuais + 1;
                return termoEncontrado;
            } 
            
            return this.documentoFabrica.CriarTermoAutuacao(processo);
        }
    }
}