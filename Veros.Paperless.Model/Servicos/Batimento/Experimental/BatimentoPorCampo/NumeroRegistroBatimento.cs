namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorCampo
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Servicos.Batimento;

    public class NumeroRegistroBatimento : ModuloBatimentoCampo
    {
        public NumeroRegistroBatimento(BatimentoFullText batimentoFullText) :
            base(batimentoFullText)
        {
        }

        public override bool EstaBatido(
            Indexacao indexacao, 
            IList<PalavraReconhecida> palavrasReconhecidas)
        {
            if (indexacao == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(indexacao.SegundoValor))
            {
                return false;
            }
            
            //// numeros muito pequenos não serão batidos, pois a chance de falso positivo eh muito grande
            var valorCadastro = indexacao.SegundoValor.Trim().ToLower().ObterInteirosRegistroRg();
            if (valorCadastro.Length < 3)
            {
                return false;
            }
            
            double numeroCadastro;
            var ehumNumero = double.TryParse(valorCadastro, out numeroCadastro);

            for (int i = 0; i < palavrasReconhecidas.Count(); i++)
            {
                var palavraAtual = palavrasReconhecidas[i].Texto.ObterInteirosRegistroRg();
                if (!string.IsNullOrEmpty(palavraAtual))
                {
                    if (palavraAtual == valorCadastro)
                    {
                        return true;
                    }
                    
                    //// checa se houve quebra de palavra no numero procurado (espaço no meio do CPF, por exemplo)
                    var incremento = 1;
                    while (palavraAtual.Length < valorCadastro.Length)
                    {
                        if (i + incremento < palavraAtual.Length - 1)
                        {
                            palavraAtual = palavraAtual + palavrasReconhecidas[i + incremento].Texto.ObterInteirosRegistroRg();
                            incremento++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (palavraAtual.IndexOf(valorCadastro) >= 0)
                    {
                        return true;
                    }

                    double numeroEncontrado;
                    if (ehumNumero && double.TryParse(palavraAtual, out numeroEncontrado))
                    {
                        if (numeroEncontrado == numeroCadastro)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}