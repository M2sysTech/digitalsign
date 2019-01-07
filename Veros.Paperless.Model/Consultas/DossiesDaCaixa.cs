namespace Veros.Paperless.Model.Consultas
{
    using System;
    using Entidades;

    public class DossiesDaCaixa
    {
        public int Id
        {
            get;
            set;
        }

        public string Caixa
        {
            get;
            set;
        }

        public DateTime Data
        {
            get;
            set;
        }

        public string Dossie
        {
            get;
            set;
        }

        public int DossieId
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string BarcodeCef
        {
            get;
            set;
        }

        public string StatusDescricao()
        {
            var status = ProcessoStatus.FromValue(this.Status);
            return status.DisplayName;
        }
    }
}