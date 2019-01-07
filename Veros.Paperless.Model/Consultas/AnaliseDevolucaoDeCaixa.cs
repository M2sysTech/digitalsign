namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class AnaliseDevolucaoDeCaixa
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

        public int Excluidos
        {
            get;
            set;
        }

        public int Processados
        {
            get;
            set;
        }

        public int EmProcessamento
        {
            get;
            set;
        }

        public int Aprovados
        {
            get;
            set;
        }

        public string PodeDevolver
        {
            get
            {
                return this.CaixaAprovada ? "SIM" : "NÃO";
            }
        }

        public bool CaixaAprovada
        {
            get
            {
                return this.TotalDossies == this.Aprovados;
            }
        }
    }
}