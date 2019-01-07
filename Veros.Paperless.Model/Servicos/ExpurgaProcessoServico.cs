namespace Veros.Paperless.Model.Servicos
{
    using Repositorios;
    using Veros.Paperless.Model.Entidades;

    /// <summary>
    /// expurgo padrao
    /// </summary>
    public class ExpurgaProcessoServico : IExpurgaProcessoServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IApagaArquivoFileTransferServico apagarArquivoFileTransferServico;

         public ExpurgaProcessoServico(
            IProcessoRepositorio processoRepositorio,
            IIndexacaoRepositorio indexacaoRepositorio,
            IApagaArquivoFileTransferServico apagarArquivoFileTransferServico)
        {
            this.processoRepositorio = processoRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.apagarArquivoFileTransferServico = apagarArquivoFileTransferServico;
        }

        public void Executar(Processo processo)
        {
            ////this.processoRepositorio.AlterarStatus(processo.Id, ProcessoStatus.AguardandoExpurgo);

            ////foreach (var documento in processo.Documentos)
            ////{
            ////    foreach (var indexacao in documento.Indexacao)
            ////    {
            ////        this.indexacaoRepositorio.Apagar(indexacao);
            ////    }

            ////    foreach (var pagina in documento.Paginas)
            ////    {
            ////        this.apagarArquivoFileTransferServico.ApagarArquivo(pagina.Id, pagina.TipoArquivo);
            ////    }
            ////}

            ////this.processoRepositorio.AlterarStatus(processo.Id, ProcessoStatus.ExpurgoRealizado);
        }
    }
}