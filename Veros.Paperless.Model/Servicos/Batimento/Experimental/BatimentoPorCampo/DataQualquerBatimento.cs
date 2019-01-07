namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorCampo
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Servicos.Batimento;

    public class DataQualquerBatimento : ModuloBatimentoCampo
    {
        public DataQualquerBatimento(BatimentoFullText batimentoFullText) :
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
            
            //// tamanho minimo da data para batimento
            var valorCadastro = indexacao.SegundoValor.Trim().ToLower();
            if (valorCadastro.Length < 8)
            {
                return false;
            }
            
            for (int i = 0; i < palavrasReconhecidas.Count(); i++)
            {
                var palavraAtual = palavrasReconhecidas[i].Texto.Trim().ToLower();
                if (!string.IsNullOrEmpty(palavraAtual))
                {
                    if (palavraAtual == valorCadastro)
                    {
                        return true;
                    }
                    
                    //// checa se houve quebra de palavra no numero procurado (espaço no meio da data, por exemplo)
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
                }
            }

            return false;
        }
    }
}