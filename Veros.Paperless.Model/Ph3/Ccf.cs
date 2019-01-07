namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;
    
    public class Ccf
    {
        [XmlElement("CODIGO_BANCO")]
        public string CodigoBanco { get; set; }

        [XmlElement("CODIGO_AGENCIA")]
        public string CodigoAgencia { get; set; }

        [XmlElement("QUANTIDADE")]
        public string Quantidade { get; set; }

        [XmlElement("ULTIMA_OCORRENCIA")]
        public string UltimaOcorrencia { get; set; }
    }
}