namespace Veros.Paperless.Model.Servicos.Separacao
{
    using ViewModel;

    public interface IRemovePdfSeparadoServico
    {
        void Executar(LoteParaSeparacaoViewModel loteParaSeparacao);
    }
}
