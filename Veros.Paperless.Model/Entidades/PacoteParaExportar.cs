namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable, XmlRoot(DataType = "", ElementName = "Lote", Namespace = "")]
    public class PacoteParaExportar
    {
        public PacoteParaExportar()
        {
            this.Dossies = new List<Dossie>();
        }

        [XmlElement("Identificacao")]
        public string Identificacao
        {
            get; 
            set;
        }

        [XmlElement("Data")]
        public DateTime Data
        { 
            get; 
            set; 
        }

        [XmlElement("Regiao")]
        public string Regiao
        {
            get; 
            set;
        }

        [XmlElement("Dossies")]
        public List<Dossie> Dossies
        {
            get; 
            set;
        }

        public void AdicionarDossier(Dossie dossie)
        {
            this.Dossies.Add(dossie);
        }
    }
}