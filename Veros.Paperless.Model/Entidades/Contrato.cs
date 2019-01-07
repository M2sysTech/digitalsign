namespace Veros.Paperless.Model.Entidades
{
    public class Contrato
    {
        public int Id
        {
            get;
            set;
        }

        public int ProcessoId
        {
            get;
            set;
        }

        public string Identificacao
        {
            get;
            set;
        }

        public string[] ArquivosCapturados
        {
            get;
            set;
        }

        public bool Finalizado
        {
            get;
            set;
        }

        public string DataInicio
        {
            get;
            set;
        }

        public string TempoProcessamento
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string Usuario
        {
            get; 
            set;
        }

        public string Loja
        {
            get; 
            set;
        }

        public string ClassificacaoRegra
        {
            get; 
            set;
        }
    }
}