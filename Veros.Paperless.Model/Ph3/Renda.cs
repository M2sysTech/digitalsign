namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class Renda
    {
        [XmlElement("FAIXA_INICIAL")]
        public string FaixaInicial { get; set; }

        [XmlElement("FAIXA_FINAL")]
        public string FaixaFinal { get; set; }
    }
}