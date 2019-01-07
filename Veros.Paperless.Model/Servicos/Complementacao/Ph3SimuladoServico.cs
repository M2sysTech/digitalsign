namespace Veros.Paperless.Model.Servicos.Complementacao
{
    using System.Xml;
    using System.Xml.Serialization;
    using Ph3;
    using Veros.Paperless.Model.Consultas;

    public class Ph3SimuladoServico
    {
        private readonly IPh3Consulta ph3Consulta;

        public Ph3SimuladoServico(IPh3Consulta ph3Consulta)
        {
            this.ph3Consulta = ph3Consulta;
        }

        public XmlElement Obter(string cpf)
        {
            var ph3 = this.ph3Consulta.Obter(cpf);

            var doc = new XmlDocument();
            
            using (var writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(typeof(ConsultaPf)).Serialize(writer, ph3);
            }

            return doc.DocumentElement;
        }
    }
}