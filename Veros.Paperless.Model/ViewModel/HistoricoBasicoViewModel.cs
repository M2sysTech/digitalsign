namespace Veros.Paperless.Model.ViewModel
{
    using System;

    public class HistoricoBasicoViewModel
    {
        public string NivelDeLog { get; set; }

        public int Codigo { get; set; }

        public int CodigoDoUsuario { get; set; }

        public DateTime? Data { get; set; }

        public string AcaoLogDocumento { get; set; }

        public string LogDocumentoObservacao { get; set; }

        public string Usuario { get; set; }
    }
}
