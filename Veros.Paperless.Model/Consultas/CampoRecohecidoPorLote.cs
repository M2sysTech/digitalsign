namespace Veros.Paperless.Model.Consultas
{
    public class CampoRecohecidoPorLote
    {
        public int LoteId
        {
            get;
            set;
        }

        public int DocumentosDoLote
        {
            get;
            set;
        }

        public int CamposReconheciveis
        {
            get;
            set;
        }

        public int QuantidadeLicencasConsumidas
        {
            get;
            set;
        }

        public int CamposBatidos
        {
            get;
            set;
        }

        public double PercentualBatimento
        {
            get
            {
                return (double)this.CamposBatidos / this.CamposReconheciveis;
            }
        }
    }
}