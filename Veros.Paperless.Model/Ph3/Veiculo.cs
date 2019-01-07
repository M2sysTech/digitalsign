namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class Veiculo
    {
        [XmlElement("PLACA")]
        public string Placa { get; set; }

        [XmlElement("ANO_FAB")]
        public string AnoFabricacao { get; set; }

        [XmlElement("ANO_MOD")]
        public string AnoModelo { get; set; }

        [XmlElement("NOME")]
        public string Nome { get; set; }

        [XmlElement("MARCA")]
        public string Marca { get; set; }

        [XmlElement("DATA")]
        public string Data { get; set; }

        [XmlElement("MODELO")]
        public string Modelo { get; set; }
    }
}