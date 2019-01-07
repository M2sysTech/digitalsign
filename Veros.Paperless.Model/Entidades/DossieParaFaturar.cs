namespace Veros.Paperless.Model.Entidades
{
    using System;

    public class DossieParaFaturar
    {
        public DateTime LoteProcessadoEm
        {
            get;
            set;
        }

        public string CaixaOrigem
        {
            get;
            set;
        }

        public string UnidadeOrigem
        {
            get;
            set;
        }

        public string NumeroDossie
        {
            get;
            set;
        }

        public string AgenteFinanceiro
        {
            get;
            set;
        }

        public int QuantidadePaginas
        {
            get;
            set;
        }
    }
}