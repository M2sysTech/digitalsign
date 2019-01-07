namespace Veros.Paperless.Model.Ph3
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Veiculos
    {
        [XmlElement("VEICULO")]
        public List<Veiculo> Veiculo { get; set; }
    }
}