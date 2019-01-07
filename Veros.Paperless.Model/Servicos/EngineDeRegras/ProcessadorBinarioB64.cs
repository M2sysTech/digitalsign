namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Comparacao;
    using Entidades;

    /// <summary>
    /// TODO: teste
    /// </summary>
    public class ProcessadorBinarioB64 : IProcessadorDeBinario
    {
        private readonly CriadorDeComparador criadorDeComparador;

        public ProcessadorBinarioB64(CriadorDeComparador criadorDeComparador)
        {
            this.criadorDeComparador = criadorDeComparador;
        }

        public ResultadoCondicaoDeRegra Processar(Processo processo, Regra regra, RegraCondicional condicao)
        {
            return new ResultadoCondicaoDeRegra(this.AtendeComparacaoEntreValorDosCampos(processo, condicao));
        }

        private bool AtendeComparacaoEntreValorDosCampos(Processo processo, RegraCondicional condicao)
        {
            //// TODO: Verificar quando o processo não possui um campo do tipo solicitado na regra
            var valorPrincipal = condicao.ObterValor(processo);
            var valorParaComparar = this.ObterValorDoCampoDeComparacao(processo, condicao);

            ////if (string.IsNullOrEmpty(valorParaComparar))
            ////{
            ////    return false;
            ////}

            if (condicao.Conectivo == ConectivoDeComparacao.Menor)
            {
                return this.criadorDeComparador
                    .CriaEncontrandoTipoDado(valorPrincipal)
                    .EhMenor(valorPrincipal, valorParaComparar);
            }

            if (condicao.Conectivo == ConectivoDeComparacao.Maior)
            {
                return this.criadorDeComparador
                    .CriaEncontrandoTipoDado(valorPrincipal)
                    .EhMaior(valorPrincipal, valorParaComparar);
            }

            if (condicao.Conectivo == ConectivoDeComparacao.Igual)
            {
                return this.criadorDeComparador
                    .CriaEncontrandoTipoDado(valorPrincipal)
                    .SaoIguais(valorPrincipal, valorParaComparar);
            }

            if (condicao.Conectivo == ConectivoDeComparacao.MenorOuIgual)
            {
                return this.criadorDeComparador
                    .CriaEncontrandoTipoDado(valorPrincipal)
                    .EhMenorOuIgual(valorPrincipal, valorParaComparar);
            }

            if (condicao.Conectivo == ConectivoDeComparacao.MaiorOuIgual)
            {
                return this.criadorDeComparador
                    .CriaEncontrandoTipoDado(valorPrincipal)
                    .EhMaiorOuIgual(valorPrincipal, valorParaComparar);
            }

            if (condicao.Conectivo == ConectivoDeComparacao.Diferente)
            {
                ////TODO: Verificar forma melhor de fazer isso
                if (condicao.Campo != null && condicao.Campo.TipoDado == TipoDado.CodigoDeBarras)
                {
                    return this.criadorDeComparador
                        .Cria(condicao.Campo.TipoDado)
                        .SaoIguais(valorPrincipal, valorParaComparar) == false;
                }

                return this.criadorDeComparador
                    .CriaEncontrandoTipoDado(valorPrincipal)
                    .SaoIguais(valorPrincipal, valorParaComparar) == false;
            }

            if (condicao.Conectivo == ConectivoDeComparacao.In)
            {
                return this.criadorDeComparador
                    .CriaEncontrandoTipoDado("TEXTO")
                    .Contem(valorPrincipal, valorParaComparar);
            }

            if (condicao.Conectivo == ConectivoDeComparacao.PrimeiroeUltimo)
            {
                return this.criadorDeComparador
                    .Cria(TipoDado.PrimeiroEUltimo)
                    .SaoIguais(valorPrincipal, valorParaComparar);
            }
            
            return false;
        }

        private string ObterValorDoCampoDeComparacao(Processo processo, RegraCondicional condicao)
        {
            var tipoDado = TipoDado.Numeric;
            var valorParaComparar = condicao.ObterValorParaComparar(processo);
            var indexacaoParaComparar = condicao.ObterIndexacaoParaComparar(processo);

            if (indexacaoParaComparar != null)
            {
                tipoDado = indexacaoParaComparar.Campo.TipoDado;
            }

            if (condicao.DeveAplicarFatorMatematico())
            {
                return FatorMatematico.Aplicar(
                    valorParaComparar,
                    condicao.OperadorMatematico,
                    condicao.FatorMatematico,
                    tipoDado);
            }

            return valorParaComparar;
        }
    }
}