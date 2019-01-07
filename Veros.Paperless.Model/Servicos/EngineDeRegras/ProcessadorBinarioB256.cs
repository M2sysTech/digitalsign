namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using System.Linq;
    using Entidades;

    public class ProcessadorBinarioB256 : IProcessadorDeBinario
    {
        public ResultadoCondicaoDeRegra Processar(Processo processo, Regra regra, RegraCondicional condicao)
        {
            return new ResultadoCondicaoDeRegra(this.CalcularQuantidadeDeDocumentos(processo, condicao));
        }

        private double CalcularQuantidadeDeDocumentos(Processo processo, RegraCondicional condicao)
        {
            return processo.Documentos.Count(x => x.TipoDocumento == condicao.TipoDocumento);
        }
    }
}