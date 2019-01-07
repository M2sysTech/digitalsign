namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable, XmlRoot(DataType = "", ElementName = "Dossie", Namespace = "")]
    public class Dossie
    {        
        public Dossie()
        {
            this.ItensDocumentais = new List<ItemDocumental>();
        }

        [XmlElement("NumeroContrato")]
        public string NumeroContrato
        {
            get;
            set;
        }

        [XmlElement("TipoDossieId")]
        public int TipoDossieId
        {
            get;
            set;
        }

        [XmlElement("Sequencial")]
        public int Sequencial
        {
            get;
            set;
        }

        [XmlElement("TipoDossieDescricao")]
        public string TipoDossieDescricao
        {
            get;
            set;
        }

        [XmlElement("QuantidadePaginas")]
        public int QuantidadePaginas
        {
            get;
            set;
        }

        [XmlElement("DataGeracaoXml")]
        public DateTime DataGeracaoXml
        {
            get;
            set;
        }

        [XmlElement("ItensDocumentais")]
        public List<ItemDocumental> ItensDocumentais
        {
            get;
            set;
        }
        
        [XmlIgnore]
        public int ProcessoId
        {
            get;
            set;
        }

        public void AdicionarItemDocumental(ItemDocumental itemDocumental)
        {
            this.ItensDocumentais.Add(itemDocumental);
        }
    }
}