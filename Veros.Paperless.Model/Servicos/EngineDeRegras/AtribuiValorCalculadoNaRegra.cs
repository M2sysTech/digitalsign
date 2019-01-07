namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using System.Linq;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class AtribuiValorCalculadoNaRegra : IAtribuiValorCalculadoNaRegra
    {
        private readonly IRegraAcaoRepositorio regraAcaoRespositorio;

        public AtribuiValorCalculadoNaRegra(IRegraAcaoRepositorio regraAcaoRespositorio)
        {
            this.regraAcaoRespositorio = regraAcaoRespositorio;
        }

        public void Executar(ResultadoDasCondicoes resultadoDasCondicoes, Processo processo)
        {
            if (string.IsNullOrEmpty(resultadoDasCondicoes.ResultadoCalculado))
            {
                return;
            }

            var acao = this.regraAcaoRespositorio.ObterAcaoPorRegra(resultadoDasCondicoes.RegraViolada.Regra.Id);

            if (acao == null || acao.Destino == null)
            {
                return;
            }

            foreach (var documento in processo.Documentos.Where(x => x.Indexacao != null && x.Indexacao.Any(i => i.Campo.Id == acao.Destino.Id)))
            {
                var indexador = documento.Indexacao.FirstOrDefault(x => x.Campo.Id == acao.Destino.Id);

                switch (acao.ColunaDestino)
                {
                    case ColunaDestino.Valor1:
                        indexador.PrimeiroValor = resultadoDasCondicoes.ResultadoCalculado;
                        break;

                    case ColunaDestino.Valor2:
                        indexador.SegundoValor = resultadoDasCondicoes.ResultadoCalculado;
                        break;

                    case ColunaDestino.ValorFinal:
                        indexador.ValorFinal = resultadoDasCondicoes.ResultadoCalculado;
                        break;
                }
            }
        }
    }
}
