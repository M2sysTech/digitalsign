namespace Veros.Paperless.Model.Servicos.Separacao
{
    using ViewModel;

    public interface IObtemLoteParaSeparacaoServico
    {
        LoteParaSeparacaoViewModel Executar(int processoId);
    }
}
