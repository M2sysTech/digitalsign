namespace Veros.Paperless.Model.ViewModel
{
    using System;
    using System.Collections.Generic;
    using Entidades;

    public class FolhaDePreparoViewModel
    {
        public int PacoteId { get; set; }

        public string BarcodeCaixa { get; set; }

        public DateTime DataPreparo { get; set; }

        public int QuantidadeDossies { get; set; }

        public string MatriculaPreparador { get; set; }

        public IList<DossieEsperado> Dossies { get; set; }
    }
}
