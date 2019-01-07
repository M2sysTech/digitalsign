namespace Veros.Paperless.Model.Servicos.Neurotech
{
    using System;

    public class RegrasNeurotech
    {
        public string Nome
        {
            get; 
            set;
        }

        public int? Carencia
        {
            get; 
            set;
        }

        public bool CarenciaAtiva
        {
            get; 
            set;
        }

        public string Descricao
        {
            get; 
            set;
        }

        public string DescricaoAnalitica
        {
            get; 
            set;
        }

        public string DescricaoSintetica
        {
            get; 
            set;
        }

        public long? Duracao
        {
            get; 
            set;
        }

        public DateTime? EndTime
        {
            get; 
            set;
        }

        public string FlagStatus
        {
            get; 
            set;
        }

        public string RegraPai
        {
            get; 
            set;
        }

        public DateTime? StartTime
        {
            get; 
            set;
        }
    }
}