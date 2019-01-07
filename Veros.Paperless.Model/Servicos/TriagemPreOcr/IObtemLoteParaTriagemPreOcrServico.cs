namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using ViewModel;

    public interface IObtemLoteParaTriagemPreOcrServico
    {
        LoteTriagemViewModel Obter(int processoId, bool ignorarExcluidas, string fase);
    }
}
