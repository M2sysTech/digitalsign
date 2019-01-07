namespace Veros.Paperless.Model.Ph3
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Ccfs
    {
        public Ccfs()
        {
            this.Ccf = new List<Ccf>();
        }

        [XmlElement("CCF")]
        public List<Ccf> Ccf { get; set; }
    }
}