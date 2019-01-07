namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    public interface IExecutorDeExpressoes
    {
        string ExecutarFormula(int regraId, string expressao);

        bool ExecutarBooleano(int regraId, string expressao);
    }
}