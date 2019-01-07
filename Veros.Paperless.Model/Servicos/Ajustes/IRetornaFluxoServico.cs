namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using ViewModel;

    public interface IRetornaFluxoServico
    {
        void Executar(int processoId, string operacao, string observacao, string acoes);
    }
}
