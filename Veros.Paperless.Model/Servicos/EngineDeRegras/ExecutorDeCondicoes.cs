namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;
    using Repositorios;

    public class ExecutorDeCondicoes : IExecutorDeCondicoes
    {
        private readonly IRegraCondicionalRepositorio regraCondicionalRepositorio;
        private readonly IValidaCondicaoDeRegra validaCondicaoDeRegra;
        private readonly IExecutorDeExpressoes executorDeExpressoes;

        public ExecutorDeCondicoes(
            IRegraCondicionalRepositorio regraCondicionalRepositorio,
            IValidaCondicaoDeRegra validaCondicaoDeRegra,
            IExecutorDeExpressoes executorDeExpressoes)
        {
            this.regraCondicionalRepositorio = regraCondicionalRepositorio;
            this.validaCondicaoDeRegra = validaCondicaoDeRegra;
            this.executorDeExpressoes = executorDeExpressoes;
        }

        public ResultadoDasCondicoes Executar(Processo processo, Regra regra)
        {
            var condicoesDaRegra = this.regraCondicionalRepositorio.ObterPorRegraId(regra.Id);
            var somaBinario = 0;
            var condicoesAtendidas = false;
            var resultadoDaExpressao = string.Empty;
            var expressao = string.Empty;

            foreach (var condicao in condicoesDaRegra)
            {
                var resultadoCondicao = this.validaCondicaoDeRegra.Validar(processo, regra, condicao);

                condicoesAtendidas = false;

                if (resultadoCondicao.FoiAtendida())
                {
                    condicoesAtendidas = true;
                    somaBinario += condicao.Binario.Value;

                    if (regra.ConectivoLogico == ConectivoLogico.Ou)
                    {
                        break;
                    }
                }

                if (condicoesAtendidas == false && regra.ConectivoLogico == ConectivoLogico.E)
                {
                    break;
                }

                if (regra.ConectivoLogico == ConectivoLogico.Complexo)
                {
                    expressao += string.Format("{0} ", resultadoCondicao.Resultado);
                }
            }

            if (regra.ConectivoLogico == ConectivoLogico.Complexo && string.IsNullOrEmpty(expressao) == false)
            {
                if (regra.RegraDeAtribuicao())
                {
                    resultadoDaExpressao = this.executorDeExpressoes.ExecutarFormula(regra.Id, expressao.Trim());
                    condicoesAtendidas = false;
                }
                else
                {
                    condicoesAtendidas = this.executorDeExpressoes.ExecutarBooleano(regra.Id, expressao.Trim());
                }
            }

            return new ResultadoDasCondicoes(
                processo,
                regra,
                somaBinario,
                condicoesAtendidas,
                resultadoDaExpressao);
        }
    }
}
