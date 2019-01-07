namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class HistoricoProfissional
    {
        [XmlElement("ITEM_PROFISSIONAL")]
        public ItemProfissional HistoricoCcfs { get; set; }
    }
}