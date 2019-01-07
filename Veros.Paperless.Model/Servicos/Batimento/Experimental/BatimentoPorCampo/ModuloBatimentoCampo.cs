namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorCampo
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;

    public abstract class ModuloBatimentoCampo : IBatimentoFullTextCampo
    {
        protected readonly BatimentoFullText batimentoFullText;
        protected IBatimentoFullTextCampo proximaTentativa;

        protected ModuloBatimentoCampo(BatimentoFullText batimentoFullText)
        {
            this.batimentoFullText = batimentoFullText;
        }

        public abstract bool EstaBatido(
            Indexacao indexacao, 
            IList<PalavraReconhecida> palavrasReconhecidas);

        public IBatimentoFullTextCampo ProximaTentativa(
            IBatimentoFullTextCampo batimentoFullTextCampo)
        {
            this.proximaTentativa = batimentoFullTextCampo;
            return batimentoFullTextCampo;
        }
    }
}