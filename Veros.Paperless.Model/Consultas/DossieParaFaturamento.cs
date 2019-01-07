namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class DossieParaFaturamento
    {
        public DateTime DataPacote
        {
            get;
            set;
        }

        public string Caixa
        {
            get;
            set;
        }

        public string Origem
        {
            get;
            set;
        }

        public string Dossie
        {
            get;
            set;
        }

        public DateTime? DataPacoteAprovado
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
