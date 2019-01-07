namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorCampo
{
    using System;
    using System.Collections.Generic;
    using Comparacao;
    using Entidades;
    using Framework;

    public class NomeMaeBatimento : ModuloBatimentoCampo
    {
        public NomeMaeBatimento(BatimentoFullText batimentoFullText) : base(batimentoFullText)
        {
        }

        public override bool EstaBatido(
            Indexacao indexacao, 
            IList<PalavraReconhecida> palavrasReconhecidas)
        {
            var listaLimpa = this.ObterListaLimpaDePalavras(palavrasReconhecidas);

            var valorCadastro = indexacao.ObterValorParaBatimento();

            if (string.IsNullOrEmpty(valorCadastro))
            {
                return false;
            }

            valorCadastro = valorCadastro
                .RemoveAcentuacao()
                .ToLower()
                .Trim();

            var listaNomes = valorCadastro
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var nomesNoCadastro = listaNomes;
        
            var encontrou = false;
            var ultimaPosicaoEncontrada = 0;
            var contadorMatch = 0;
            var posInicial = 0;

            var batido = false;

            foreach (var nomeNoCadastro in nomesNoCadastro)
            {
                for (var i = posInicial; i < listaLimpa.Count; i++)
                {
                    var palavraReconhecida = listaLimpa[i].Texto.Trim().ToLower();

                    ////primeiro Nome
                    if (!encontrou)
                    {
                        if (palavraReconhecida.Equals(nomeNoCadastro))
                        {
                            encontrou = true;
                            ultimaPosicaoEncontrada = i;
                            contadorMatch++;
                            posInicial = i + 1;
                            break;
                        }
                    }
                    else
                    {
                        //// segundo nome em diante
                        if (palavraReconhecida.Equals(nomeNoCadastro))
                        {
                            if (i - 1 != ultimaPosicaoEncontrada)
                            {
                                encontrou = false;
                                break;
                            }

                            ultimaPosicaoEncontrada = i;
                            contadorMatch++;
                            posInicial = i + 1;
                            break;
                        }
                        else
                        {
                            encontrou = false;
                            break;
                        }
                    }
                }
            }

            batido = contadorMatch == nomesNoCadastro.Length;

            if (batido == false)
            {
                if (this.proximaTentativa.NaoTemConteudo() == false)
                {
                    batido = this.proximaTentativa
                        .EstaBatido(indexacao, palavrasReconhecidas);
                }
            }
            
            return batido;
        }

        private List<dynamic> ObterListaLimpaDePalavras(IList<PalavraReconhecida> palavras)
        {
            var listaLimpa = new List<dynamic>();

            if (palavras != null)
            {
                foreach (var regiao in palavras)
                {
                    var palavraAtual = Equivalencia.LimpaConteudoNome(regiao.Texto);
                    if (string.IsNullOrEmpty(palavraAtual) == false)
                    {
                        ////listaLimpa.Add(new dynamic
                        ////{
                        ////    Texto = palavraAtual,
                        ////    Localizacao = regiao.Localizacao
                        ////});
                    }
                }
            }

            return listaLimpa;
        }
    }
}