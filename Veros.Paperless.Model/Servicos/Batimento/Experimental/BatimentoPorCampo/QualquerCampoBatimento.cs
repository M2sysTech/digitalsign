namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorCampo
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;

    public class QualquerCampoBatimento : ModuloBatimentoCampo
    {
        public QualquerCampoBatimento(BatimentoFullText batimentoFullText) : 
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

            var listaNomes = nomeCompleto.Split(' ').ToList();

            var encontrou = false;
            var ultimaPosicaoEncontrada = 0;
            var contadorMatch = 0;
            var posInicial = 0;

            foreach (var nomeNoCadastro in listaNomes)
            {
                for (int i = posInicial; i < palavrasParaAnaliseFullTextGenerica.Count; i++)
                {
                    var palavraReconhecida = palavrasParaAnaliseFullTextGenerica[i].Texto.Trim().ToLower();

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
                        if (palavraReconhecida.Equals(nomeNoCadastro))
                        {
                            if (i - 1 != ultimaPosicaoEncontrada)
                            {
                                return false;
                            }

                            ultimaPosicaoEncontrada = i;
                            contadorMatch++;
                            posInicial = i + 1;
                            break;
                        }

                        return false;
                    }
                }

                if (!encontrou)
                {
                    return false;
                }
            }

            return true;
        }
    }
}