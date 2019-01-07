namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorCampo
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Framework;
    using Veros.Paperless.Model.Entidades;

    public class LogradouroBatimento : ModuloBatimentoCampo
    {
        public LogradouroBatimento(BatimentoFullText batimentoFullText) : 
            base(batimentoFullText)
        {
        }

        public override bool EstaBatido(
            Indexacao indexacao, 
            IList<PalavraReconhecida> palavrasReconhecidas)
        {
            if (string.IsNullOrEmpty(indexacao.ObterValorParaBatimento()))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(indexacao.ObterValorParaBatimento()))
            {
                return false;
            }

            var palavrasParaAnaliseFullTextGenerica = this.batimentoFullText
                .ExtrairTextoGenerico(palavrasReconhecidas);

            var nomeCompleto = indexacao
                .ObterValorParaBatimento()
                .RemoveAcentuacao()
                .ToLower()
                .Trim()
                .Split(' ')
                .ToList();
            
            var contadorMatch = 0;

            foreach (var nomeNoCadastro in nomeCompleto)
            {
                var resultado = palavrasParaAnaliseFullTextGenerica.Any(x => x.Texto.RemoveAcentuacao() == nomeNoCadastro);

                if (resultado)
                {
                    contadorMatch++;
                }

                if (contadorMatch >= 2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}