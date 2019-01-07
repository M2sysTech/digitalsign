namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorCampo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;

    public class NomePaiBatimento : ModuloBatimentoCampo
    {
        public NomePaiBatimento(BatimentoFullText batimentoFullText) :
            base(batimentoFullText)
        {
        }

        public override bool EstaBatido(Indexacao indexacao, IList<PalavraReconhecida> palavrasReconhecidas)
        {
            var palavrasParaAnaliseFullTextGenerica = this.batimentoFullText
                .ExtrairTextoGenerico(palavrasReconhecidas);

            if (indexacao.ObterValorParaBatimento().NaoTemConteudo())
            {
                return false;
            }

            var nomeCompleto = indexacao
                .ObterValorParaBatimento()
                .RemoveAcentuacao()
                .ToLower()
                .Trim();

            var listaNomes = nomeCompleto.Split(
                new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            var nomesNoCadastro = new List<string>();
            nomesNoCadastro.Add(listaNomes.First());
            nomesNoCadastro.Add(listaNomes.Last());

            var encontrou = false;
            var contadorMatch = 0;
            var posInicial = 0;
            var batido = false;

            foreach (var nomeNoCadastro in nomesNoCadastro)
            {
                for (var i = posInicial; i < palavrasParaAnaliseFullTextGenerica.Count; i++)
                {
                    var palavraReconhecida = palavrasParaAnaliseFullTextGenerica[i]
                        .Texto
                        .Trim()
                        .ToLower();

                    ////primeiro Nome
                    if (!encontrou)
                    {
                        if (palavraReconhecida.Equals(nomeNoCadastro))
                        {
                            encontrou = true;
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
                            contadorMatch++;
                            posInicial = i + 1;
                            break;
                        }
                    }
                }
            }

            batido = contadorMatch == nomesNoCadastro.Count;

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
    }
}