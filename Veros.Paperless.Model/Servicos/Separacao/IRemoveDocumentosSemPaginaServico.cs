namespace Veros.Paperless.Model.Servicos.Separacao
{
    using ViewModel;

    public interface IRemoveDocumentosSemPaginaServico
    {
        void Executar(LoteParaSeparacaoViewModel loteParaSeparacao);
    }
}
