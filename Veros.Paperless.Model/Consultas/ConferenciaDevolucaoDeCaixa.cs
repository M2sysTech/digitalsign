namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class ConferenciaDevolucaoDeCaixa
    {
        public int Id
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string Identificacao
        {
            get;
            set;
        }

        public int TotalDossies
        {
            get;
            set;
        }        
    }
}