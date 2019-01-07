namespace Veros.Paperless.Model.Servicos.Documentos
{
    using ViewModel;

    public class IncluirNovoItemComoArquivoServico : IIncluirNovoItemComoArquivoServico
    {
        private readonly IGerarNovoDocumentoServico gerarNovoDocumentoServico;

        public IncluirNovoItemComoArquivoServico(IGerarNovoDocumentoServico gerarNovoDocumentoServico)
        {
            this.gerarNovoDocumentoServico = gerarNovoDocumentoServico;
        }

        public void Incluir(UploadViewModel viewModel)
        {
            this.gerarNovoDocumentoServico.GerarArquivoUpload(viewModel.LoteId, viewModel.TypedocId, @"C:\_lixo\teste", 0);
        }
    }
}
