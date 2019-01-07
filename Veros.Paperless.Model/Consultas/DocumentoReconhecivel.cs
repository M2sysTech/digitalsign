namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public class DocumentoReconhecivel
    {
        public string Documento
        {
            get;
            set;
        }

        public IList<CampoReconhecivel> CamposReconheciveis
        {
            get;
            set;
        }
    }
}
