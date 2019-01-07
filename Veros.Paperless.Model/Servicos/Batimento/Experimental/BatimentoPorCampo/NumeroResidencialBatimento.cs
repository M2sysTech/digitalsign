namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorCampo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Comparacao;
    using Framework;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Servicos.Batimento;

    public class NumeroResidencialBatimento : ModuloBatimentoCampo
    {
        public NumeroResidencialBatimento(BatimentoFullText batimentoFullText) :
            base(batimentoFullText)
        {
        }

        public override bool EstaBatido(
            Indexacao indexacao, 
            IList<PalavraReconhecida> palavrasReconhecidas)
        {
            ////Encontra posicao da ultima palavra de rua na lista de palavras reconhecidas
            var indexacaoLogradouro = indexacao
                .Documento
                .Indexacao
                .FirstOrDefault(x => x.Campo.Id == Campo.CampoLogradouroComprovanteDeResidencia);

            if (indexacaoLogradouro.NaoTemConteudo())
            {
                return false;
            }

            if (indexacaoLogradouro.ValorFinal.NaoTemConteudo())
            {
                return false;
            }

            var posicaoNaLista = this.ObterPosicaoDoLogradouroEmPalavraReconhecida(
                indexacaoLogradouro.ValorFinal,
                palavrasReconhecidas);

            if (posicaoNaLista > 0)
            {
                var posicaoFinalNaLista = posicaoNaLista + 20 <= palavrasReconhecidas.Count ? 
                    posicaoNaLista + 20 : 
                    palavrasReconhecidas.Count;

                for (var i = posicaoNaLista; i <= posicaoFinalNaLista; i++)
                {
                    if (this.CompareNumeros(
                        indexacao.ObterValorParaBatimento().Trim(),
                        palavrasReconhecidas[i - 1].Texto))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private int ObterPosicaoDoLogradouroEmPalavraReconhecida(
            string logradouro, 
            IList<PalavraReconhecida> palavrasReconhecidas)
        {
            var palavraDoLogradouros = logradouro.Trim().Split(Convert.ToChar(" "));

            foreach (var palavraDoLogradouro in palavraDoLogradouros)
            {
                if (palavraDoLogradouro.Length < 3)
                {
                    continue;
                }

                var posicao = 0;

                foreach (var palavraReconhecida in palavrasReconhecidas)
                {
                    posicao++;

                    if (palavraReconhecida.Texto.NaoTemConteudo())
                    {
                        continue;
                    }

                    var palavraAtual = palavraReconhecida
                        .Texto
                        .ToUpper()
                        .Trim();

                    if (palavraAtual.Contains(palavraDoLogradouro.ToUpper()))
                    {
                        return posicao;
                    }
                }
            }

            return 0;
        }

        private bool CompareNumeros(string primeiroValor, string segundoValor)
        {
            var valueWords = primeiroValor.Split(Convert.ToChar(" "));
            if (valueWords.Length != 1)
            {
                return false;
            }

            double numero;
            var numeral = double.TryParse(primeiroValor, out numero);
            if (!numeral)
            {
                return false;
            }

            if (segundoValor.ToUpper().Contains(numero.ToString()))
            {
                return true;
            }

            return false;
        }
    }
}