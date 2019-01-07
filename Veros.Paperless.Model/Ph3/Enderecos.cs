namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class Enderecos
    {
        [XmlElement("RANKING")]
        public string Ranking { get; set; }

        [XmlElement("LOGRADOURO")]
        public string Logradouro { get; set; }

        [XmlElement("NUMERO")]
        public string Numero { get; set; }

        [XmlElement("COMPLEMENTO")]
        public string Complemento { get; set; }

        [XmlElement("BAIRRO")]
        public string Bairro { get; set; }

        [XmlElement("CEP")]
        public string Cep { get; set; }

        [XmlElement("CIDADE")]
        public string Cidade { get; set; }

        [XmlElement("TIPO")]
        public string Tipo { get; set; }

        [XmlElement("TITULO")]
        public string Titulo { get; set; }

        [XmlElement("UF")]
        public string Uf { get; set; }

        [XmlElement("SCORE")]
        public string Score { get; set; }
    }
}