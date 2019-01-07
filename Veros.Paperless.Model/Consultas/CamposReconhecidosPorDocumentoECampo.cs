namespace Veros.Paperless.Model.Consultas
{
    public class CamposReconhecidosPorDocumentoECampo
    {
        public string DataInicio
        {
            get;
            set;
        }

        public string DataFim
        {
            get;
            set;
        }

        public string TipoDocumento
        {
            get;
            set;
        }

        public string Campo
        {
            get;
            set;
        }

        public int Acertos
        {
            get;
            set;
        }

        public decimal Erros
        {   
            get;
            set;
        }

        public decimal Total
        {
            get;
            set;
        }

        public decimal CalculaPorcentagem(int campo)
        {
            return campo / this.Total * 100;
        }
    }
}
