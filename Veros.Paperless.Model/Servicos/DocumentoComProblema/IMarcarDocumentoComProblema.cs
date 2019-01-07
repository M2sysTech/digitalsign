namespace Veros.Paperless.Model.Servicos.DocumentoComProblema
{
    using Veros.Paperless.Model.ViewModel;

    public interface IMarcarDocumentoComProblema
    {
        void Executar(DocumentoComProblemaViewModel viewModel);
    }
}
