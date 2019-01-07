namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using System.Linq;
    using Entidades;

    public class ProcessadorBinarioB128 : IProcessadorDeBinario
    {
        public ResultadoCondicaoDeRegra Processar(Processo processo, Regra regra, RegraCondicional condicao)
        {
            return new ResultadoCondicaoDeRegra(this.CalcularSomatorioDeCampos(processo, condicao));
        }

        private double CalcularSomatorioDeCampos(Processo processo, RegraCondicional condicao)
        {
            var total = 0.0;

            foreach (var documento in processo.Documentos)
            {
                var indexacao = documento.Indexacao.FirstOrDefault(x => x.Campo == condicao.Campo);

                if (indexacao == null)
                {
                    continue;
                }
                
                var valorDoCampo = 0.0;

                if (double.TryParse(indexacao.ObterValor(), out valorDoCampo))
                {
                    total += valorDoCampo;
                }
            }

            return total;
        }
    }
}