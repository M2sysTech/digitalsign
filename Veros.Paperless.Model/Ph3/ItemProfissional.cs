namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;
    
    public class ItemProfissional
    {
        [XmlElement("CNPJ")]
        public string Cnpj { get; set; }

        [XmlElement("NOME")]
        public string Nome { get; set; }

        [XmlElement("CBO")]
        public string Cbo { get; set; }

        [XmlElement("CBO_DESCRICAO")]
        public string CboDescricao { get; set; }

        [XmlElement("RENDA_PRESUMIDA")]
        public string RendaPresumida { get; set; }
    }
}