namespace Veros.Paperless.Model.Ph3
{
    using System.Xml.Serialization;

    public class DadosCadastrais
    {
        [XmlElement("CPF")]
        public string CPf { get; set; }

        [XmlElement("NOME")]
        public string Nome { get; set; }

        [XmlElement("NOME_ULTIMO")]
        public string NomeUltimo { get; set; }

        [XmlElement("SEXO")]
        public string Sexo { get; set; }

        [XmlElement("TITULO_ELEITOR")]
        public string TituloEleitor { get; set; }

        [XmlElement("RG")]
        public string RegistroGeral { get; set; }

        [XmlElement("ESCOLARIDADE")]
        public string Escolaridade { get; set; }

        [XmlElement("NUM_DEPENDENTES")]
        public string NumeroDependentes { get; set; }

        [XmlElement("COR")]
        public string Cor { get; set; }

        [XmlElement("CPF_MAE")]
        public string CpfMae { get; set; }

        [XmlElement("NOME_MAE")]
        public string NomeMae { get; set; }

        [XmlElement("DATA_NASC")]
        public string DataNascimento { get; set; }

        [XmlElement("IDADE")]
        public string Idade { get; set; }

        [XmlElement("ESTADO_CIVIL")]
        public string EstadoCivil { get; set; }

        [XmlElement("SIGNO")]
        public string Signo { get; set; }

        [XmlElement("SITUACAO_RECEITA")]
        public string SituacaoReceita { get; set; }

        [XmlElement("DATA_OBITO")]
        public string DataObito { get; set; }

        [XmlElement("CNPJ_EMPREGRADOR")]
        public string CnpjEmpregador { get; set; }

        [XmlElement("NOME_EMPREGADOR")]
        public string NomeEmpregador { get; set; }

        [XmlElement("CBO")]
        public string Cbo { get; set; }

        [XmlElement("CBO_DESCRICAO")]
        public string CboDescricao { get; set; }

        [XmlElement("RENDA_PRESUMIDA")]
        public string RendaPresumida { get; set; }

        [XmlElement("PPE")]
        public string Ppe { get; set; }
    }
}