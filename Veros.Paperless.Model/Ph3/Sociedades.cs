namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class Sociedades
    {
        [XmlElement("CNPJ")]
        public string Cnpj { get; set; }

        [XmlElement("CPF")]
        public string Cpf { get; set; }

        [XmlElement("NOME_EMPRESA")]
        public string NomeEmpresa { get; set; }

        [XmlElement("NOME_SOCIO")]
        public string NomeSocio { get; set; }

        [XmlElement("DATA_ENTRADA")]
        public string DataEntrada { get; set; }

        [XmlElement("POSICAO")]
        public string Posicao { get; set; }

        [XmlElement("PARTICIPACAO")]
        public string Participacao { get; set; }
    }
}