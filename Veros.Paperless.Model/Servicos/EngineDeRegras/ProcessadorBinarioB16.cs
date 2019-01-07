namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;

    public class ProcessadorBinarioB16 : IProcessadorDeBinario
    {
        public ResultadoCondicaoDeRegra Processar(Processo processo, Regra regra, RegraCondicional condicao)
        {
            return new ResultadoCondicaoDeRegra(processo.PossuiCampoComValorPreenchido(condicao.Campo));
        }
    }
}