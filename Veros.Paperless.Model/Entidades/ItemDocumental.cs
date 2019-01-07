namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Xml.Serialization;

    [Serializable, XmlRoot(DataType = "", ElementName = "ItemDocumental", Namespace = "")]
    public class ItemDocumental
    {
        [XmlElement("NumeroDossie")]
        public string NumeroDossie
        {
            get;
            set;
        }

        [XmlElement("TipoDocumento")]
        public string TipoDocumento
        {
            get;
            set;
        }

        [XmlElement("NomeParaArquivo")]
        public string NomeParaArquivo
        {
            get;
            set;
        }

        [XmlIgnore]
        public int DocumentoId
        {
            get;
            set;
        }
    }
}