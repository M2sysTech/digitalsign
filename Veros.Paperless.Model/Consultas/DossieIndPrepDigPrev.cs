namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class DossieIndPrepDigPrev
    {
        public DateTime DataColeta { get; set; }

        public int Caixas { get; set; }

        public int DossiesFinalizado { get; set; }

        public int PaginasDossiesFinalizado { get; set; }

        public int DossiesAguardandoDig { get; set; }

        public int PaginasDossiesAguardandoDig { get; set; }

        public int DossiesEmPreparo { get; set; }

        public int DossiesPrevisto { get; set; }
    }
}