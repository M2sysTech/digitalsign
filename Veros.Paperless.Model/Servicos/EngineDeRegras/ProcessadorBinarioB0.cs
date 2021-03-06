namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;

    public class ProcessadorBinarioB0 : IProcessadorDeBinario
    {
        public ResultadoCondicaoDeRegra Processar(Processo processo, Regra regra, RegraCondicional condicao)
        {
            return new ResultadoCondicaoDeRegra(condicao.ValorFixo);
        }
    }
}