namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class CertificadoDeEntregaConsulta 
    {
        public long LoteCefId
        {
            get;
            set;
        }

        public long QuantidadesDossies
        {
            get;
            set;
        }

        public DateTime DataFim
        {
            get;
            set;
        }

        public decimal QuantidadeDePaginas
        {
            get;
            set;
        }

        public DateTime? DataGeracaoCertificado
        {
            get;
            set;
        }

        public DateTime? DataAssinaturaCertificado
        {
            get;
            set;
        }
    }
}
