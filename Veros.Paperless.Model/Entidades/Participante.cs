namespace Veros.Paperless.Model.Entidades
{
    using System.Xml.Serialization;

    public class Participante
    {
        private string sequenciaDeTitularidade;

        public Participante()
        {
            this.Documentos = new DocumentoParticipante(); 
        }

        [XmlElement("TITOUREP")]
        public string IndicadorDoTitularOuRepresentante { get; set; }

        [XmlElement("CPF")]
        public string CpfDoTitularOuRepresentante { get; set; }

        [XmlElement("IDSEQPA")]
        public string IndicadorDeSequenciaDoParticipante { get; set; }

        [XmlElement("IDREPRL")]
        public string IndicadorDeRepresentanteLegal { get; set; }

        [XmlElement("CDPARTC")]
        public string CodigoDoParticipante { get; set; }

        [XmlElement("NOMCL")]
        public string NomeDoCliente { get; set; }

        [XmlElement("NMMAECLI")]
        public string NomeDaMaeDoCliente { get; set; }

        [XmlElement("NMPAICLI")]
        public string NomeDoPaiDoCliente { get; set; }

        [XmlElement("NMCONJ")]
        public string NomeDoConjuge { get; set; }

        [XmlElement("IDDOCLI")]
        public string NumeroDoDocumentoDeIdentificacao { get; set; }

        [XmlElement("TPDOCIDE")]
        public string TipoDeDocumentoDeIdentificacao { get; set; }

        [XmlElement("DTDOCEXP")]
        public string DataDeEmissaoDoDocumento { get; set; }

        [XmlElement("IDORGEMI")]
        public string OrgaoEmissorDoDocumento { get; set; }

        [XmlElement("IDESTEMI")]
        public string EstadoEmissorDoDocumento { get; set; }

        [XmlElement("CDESTCIV")]
        public string EstadoCivilDoParticipante { get; set; }

        [XmlElement("DTNASC")]
        public string DataDeNascimentoDoParticipante { get; set; }

        [XmlElement("NMNATUR")]
        public string CidadeDeNascimentoDoParticipante { get; set; }

        [XmlElement("ESTNASC")]
        public string EstadoDeNascimentoDoParticipante { get; set; }

        [XmlElement("PAISNASC")]
        public string PaisDeNascimentoDoParticipante { get; set; }

        [XmlElement("CDNAC")]
        public string NacionalidadeDoParticipante { get; set; }

        [XmlElement("CEP")]
        public string CepDaResidenciaDoParticipante { get; set; }

        [XmlElement("LOGRAD")]
        public string LogradouroDaResidenciaDoParticipante { get; set; }

        [XmlElement("NUMLEC")]
        public string NumeroDaResidenciaDoParticipante { get; set; }

        [XmlElement("COMPL")]
        public string ComplementoDaResidenciaDoParticipante { get; set; }

        [XmlElement("BAIRRO")]
        public string BairroDaResidenciaDoParticipante { get; set; }

        [XmlElement("CIDADE")]
        public string CidadeDaResidenciaDoParticipante { get; set; }

        [XmlElement("ESTADO")]
        public string EstadoDaResidenciaDoParticipante { get; set; }

        [XmlElement("QTDDOCS")]
        public string QuantidadeDeDocumentosDoParticipante { get; set; }

        [XmlElement("DOCUMENTOS")]
        public DocumentoParticipante Documentos { get; set; }

        public string NomeComCpf
        {
            get
            {
                return string.Format("{0} ({1})", this.NomeDoCliente, this.CpfDoTitularOuRepresentante);
            }
        }

        public string CpfComSequenciaDeTitularidade
        {
            get
            {
                return string.Format("{0}-{1}", this.CpfDoTitularOuRepresentante, this.SequenciaDeTitularidade);
            }
        }

        public string SequenciaDeTitularidade
        {
            get
            {
                return this.sequenciaDeTitularidade;
            }
        }

        public void AtualizarSequenciaDeTitularidade(string valor)
        {
            this.sequenciaDeTitularidade = valor;
        }
    }
}
