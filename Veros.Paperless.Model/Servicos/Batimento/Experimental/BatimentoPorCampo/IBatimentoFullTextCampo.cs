namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorCampo
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;

    public interface IBatimentoFullTextCampo
    {
        bool EstaBatido(Indexacao indexacao, IList<PalavraReconhecida> palavrasReconhecidas);

        IBatimentoFullTextCampo ProximaTentativa(IBatimentoFullTextCampo batimentoFullTextCampo);
    }
}