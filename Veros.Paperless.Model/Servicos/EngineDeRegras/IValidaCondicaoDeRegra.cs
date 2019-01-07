namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;

    public interface IValidaCondicaoDeRegra
    {
        ResultadoCondicaoDeRegra Validar(Processo processo, Regra regra, RegraCondicional condicao);
    }
}