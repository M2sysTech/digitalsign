namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;

    public interface IProcessadorDeBinario
    {
        ResultadoCondicaoDeRegra Processar(Processo processo, Regra regra, RegraCondicional condicao);
    }
}