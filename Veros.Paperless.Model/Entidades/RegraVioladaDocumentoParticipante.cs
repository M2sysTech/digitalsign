namespace Veros.Paperless.Model.Entidades
{
    using System.Xml.Serialization;

    public class RegraVioladaDocumentoParticipante
    {
        [XmlElement("CDDOC")]
        public string CodigoDoTipoDoDocumento { get; set; }

        [XmlElement("IDNDOC")]
        public string CodigoDoDocumento { get; set; }

        [XmlElement("STDOC")]
        public string StatusDoDocumento { get; set; }

        [XmlElement("IDVALID")]
        public string CodigoDaValidacao { get; set; }

        [XmlElement("SEL")]
        public string IndicadorDaSelecaoDeValidacao { get; set; }
    }
}
