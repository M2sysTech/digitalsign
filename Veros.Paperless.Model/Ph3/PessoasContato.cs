namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class PessoasContato
    {
        [XmlElement("CONTATO")]
        public Contato Contato { get; set; }
    }
}