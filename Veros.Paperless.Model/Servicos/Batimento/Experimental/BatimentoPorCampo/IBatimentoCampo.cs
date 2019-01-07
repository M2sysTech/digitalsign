namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorCampo
{
    using System.Collections.Generic;
    using Entidades;

    public interface IBatimentoCampo
    {
        CampoBatido Entre(
            Indexacao indexacao, 
            IList<PalavraReconhecida> palavrasReconhecidas);
    }
}