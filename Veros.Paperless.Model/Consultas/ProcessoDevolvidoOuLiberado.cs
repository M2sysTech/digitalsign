namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class ProcessoDevolvidoOuLiberado
    {
        public int LoteId
        {
            get;
            set;
        }

        public int ProcessoId
        {
            get;
            set;
        }

        public string Agencia
        {
            get;
            set;
        }

        public string Conta
        {
            get;
            set;
        }

        public DateTime DataOperacao
        {
            get;
            set;
        }

        public string AcaoRealizada
        {
            get;
            set;
        }

        public string AcaoFormatada
        {
            get
            {
                return this.AcaoRealizada == "DA" ? "Devolvida" : "Liberada";
            }
        }

        public string Operador
        {
            get;
            set;
        }
    }
}
