namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;

    public interface IExecutorDeCondicoes
    {
        ResultadoDasCondicoes Executar(Processo processo, Regra regra);
    }
}