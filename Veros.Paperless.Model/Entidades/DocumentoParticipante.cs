namespace Veros.Paperless.Model.Entidades
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class DocumentoParticipante
    {
        public DocumentoParticipante()
        {
            this.RegrasVioladas = new List<RegraVioladaDocumentoParticipante>();
        }

        [XmlElement("DOCUMENTO")]
        public List<RegraVioladaDocumentoParticipante> RegrasVioladas { get; set; }
    }
}
