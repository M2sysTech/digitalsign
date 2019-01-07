namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class Emails
    {
        [XmlElement("EMAIL")]
        public string Email { get; set; }
    }
}