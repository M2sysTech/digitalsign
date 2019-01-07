namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RelatorioFaturamento
    {
        public RelatorioFaturamento()
        {
            this.DossiesParaFaturar = new List<DossieParaFaturar>();
            this.Inicio = DateTime.Now;
            this.Termino = DateTime.Now;
        }

        public DateTime Inicio
        {
            get;
            set;
        }

        public DateTime Termino
        {
            get;
            set;
        }

        public IList<DossieParaFaturar> DossiesParaFaturar
        {
            get;
            set;
        }

        public int TotalPaginas
        {
            get
            {
                return this.DossiesParaFaturar.Sum(x => x.QuantidadePaginas);
            }
        }
    }
}