namespace Veros.Paperless.Model.Servicos.Documentos
{
    using ViewModel;

    public interface IIncluirNovoItemComoArquivoServico
    {
        void Incluir(UploadViewModel viewModel);
    }
}
