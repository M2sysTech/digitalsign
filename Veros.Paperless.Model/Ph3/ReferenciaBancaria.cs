namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class ReferenciaBancaria
    {
        [XmlElement("BANCO_CODIGO")]
        public string CodigoBanco { get; set; }

        [XmlElement("AGENCIA")]
        public string Agencia { get; set; }

        [XmlElement("TIPO")]
        public string Tipo { get; set; }
    }
}