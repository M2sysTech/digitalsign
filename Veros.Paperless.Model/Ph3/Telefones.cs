namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class Telefones
    {
        [XmlElement("RANKING")]
        public string Ranking { get; set; }

        [XmlElement("TELEFONE")]
        public string Telefone { get; set; }

        [XmlElement("DDD")]
        public string Ddd { get; set; }

        [XmlElement("NUMERO")]
        public string Numero { get; set; }
    }
}