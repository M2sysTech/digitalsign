namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class Contato
    {
        [XmlElement("CPF")]
        public string Cpf { get; set; }

        [XmlElement("NOME")]
        public string Nome { get; set; }

        [XmlElement("TIPO")]
        public string Tipo { get; set; }

        [XmlElement("PARENTESCO")]
        public string Paretesco { get; set; }
    }
}