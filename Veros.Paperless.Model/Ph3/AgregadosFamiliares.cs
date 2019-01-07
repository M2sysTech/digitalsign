namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class AgregadosFamiliares
    {
        [XmlElement("HOUSEHOLD")]
        public Contato HouseHold { get; set; }
    }
}