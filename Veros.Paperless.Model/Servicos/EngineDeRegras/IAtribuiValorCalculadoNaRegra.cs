namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Veros.Paperless.Model.Entidades;

    public interface IAtribuiValorCalculadoNaRegra
    {
        void Executar(ResultadoDasCondicoes resultadoDasCondicoes, Processo processo);
    }
}