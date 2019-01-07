namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;

    public class ProcessadorBinarioB2 : IProcessadorDeBinario
    {
        public ResultadoCondicaoDeRegra Processar(Processo processo, Regra regra, RegraCondicional condicao)
        {
            return new ResultadoCondicaoDeRegra(processo.PossuiDocumentoDoTipo(condicao.TipoDocumento) == false);
        }
    }
}