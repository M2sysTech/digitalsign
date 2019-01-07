namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class PainelDeAcompanhamentoColeta
    {
        public int Id
        {
            get;
            set;
        }

        public DateTime Data
        {
            get;
            set;
        }

        public string Uf
        {
            get;
            set;
        }

        public int QuantidadeEmAgendamento
        {
            get;
            set;
        }

        public int QuantidadeEmTransportadora
        {
            get;
            set;
        }

        public int QuantidadeEmRecepcao
        {
            get;
            set;
        }       
 
        public int QuantidadeEmConferencia
        {
            get;
            set;
        }

        public int QuantidadeEmPreparo
        {
            get;
            set;
        }

        public int QuantidadeDeOcorrencias
        {
            get;
            set;
        }        
    }
}