namespace Veros.Paperless.Model.Ph3
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable, XmlRoot(DataType = "", ElementName = "CONSULTA_PF", Namespace = "")]
    public class ConsultaPf
    {
        [XmlElement("DADOS_CADASTRAIS")]
        public DadosCadastrais DadosCadastrais { get; set; }

        [XmlElement("SCORE")]
        public string Score { get; set; }

        [XmlElement("RENDA")]
        public Renda Renda { get; set; }

        [XmlElement("REFERENCIA_BANCARIA")]
        public ReferenciaBancaria ReferenciaBancaria { get; set; }

        [XmlElement("POSSUI_SOCIEDADES")]
        public string PossuiSociedades { get; set; }

        [XmlElement("POSSUI_VEICULOS")]
        public string PossuiVeiculos { get; set; }

        [XmlElement("POSSUI_PROTESTO")]
        public string PossuiProtesto { get; set; }

        [XmlElement("SITUACO_COBRANCA")]
        public string SituacaoCobranca { get; set; }

        [XmlElement("TELEFONES")]
        public Telefones Telefones { get; set; }

        [XmlElement("ENDERECOS")]
        public Enderecos Enderecos { get; set; }
        
        [XmlElement("ENDERECOS_COMERCIAIS")]
        public Enderecos EnderecosComerciais { get; set; }

        [XmlElement("EMAILS")]
        public Emails Emails { get; set; }

        [XmlElement("PESSOAS_CONTATO")]
        public PessoasContato PessoasContato { get; set; }

        [XmlElement("AGREGADOS_FAMILIARES")]
        public AgregadosFamiliares AgregadosFamiliares { get; set; }

        [XmlElement("VEICULOS")]
        public Veiculos Veiculos { get; set; }

        [XmlElement("CCFS")]
        public Ccfs Ccfs { get; set; }

        [XmlElement("HISTORICO_CCFS")]
        public Ccfs HistoricoCcfs { get; set; }

        [XmlElement("HISTORICO_PROFISSIONAL")]
        public HistoricoProfissional HistoricoProfissional { get; set; }

        [XmlElement("SOCIEDADES")]
        public Sociedades Sociedades { get; set; }
    }
}