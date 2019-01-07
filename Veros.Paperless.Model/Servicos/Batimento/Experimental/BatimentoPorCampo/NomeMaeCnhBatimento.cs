namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorCampo
{
    using System.Collections.Generic;
    using Entidades;
    using Framework;

    public class NomeMaeCnhBatimento : ModuloBatimentoCampo
    {
        public NomeMaeCnhBatimento(BatimentoFullText batimentoFullText)
            : base(batimentoFullText)
        {
        }

        public override bool EstaBatido(
            Indexacao indexacao, 
            IList<PalavraReconhecida> palavrasReconhecidas)
        {
            if (this.PodeRealizarBatimento(indexacao) == false)
            {
                return false;
            }

            var palavrasParaAnaliseFullTextFiliacao = this.batimentoFullText
                .MontarListaFiliacaoCnh(palavrasReconhecidas);

            var nomeDaMae = this.batimentoFullText
                .ExtrairNomeMaeCompleto(palavrasParaAnaliseFullTextFiliacao);

            var batido = this.batimentoFullText
                .ComparaFiliacaoCnhPrimeiroEUltimo(indexacao, nomeDaMae);

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

        private bool PodeRealizarBatimento(Indexacao indexacao)
        {
            return indexacao.Documento.TipoDocumento.Id.Equals(TipoDocumento.CodigoCnh);
        }
    }
}