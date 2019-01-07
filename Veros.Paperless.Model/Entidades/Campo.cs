namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Linq;
    using Framework.Modelo;
    using Iesi.Collections.Generic;
    using System.Collections.Generic;

    [Serializable]
    public class Campo : Entidade
    {
        public const string ReferenciaDeArquivoNomeTitular = "NOMECLI";
        public const string ReferenciaDeArquivoCpf = "CPFBAS";
        public const string ReferenciaDeArquivoNacionalidadeDoParticipante = "CDNAC";
        public const string ReferenciaDeArquivoDataDeNascimentoDoParticipante = "DATNASC8";
        public const string ReferenciaDeArquivoNumeroDocumentoIdentificacao = "IDDOCLI";
        public const string ReferenciaDeArquivoNumeroRegistroDocumentoIdentificacao = "NUREGDI";
        public const string ReferenciaDeArquivoOrgaoEmissorDoDocumento = "IDORGEMI";
        public const string ReferenciaDeArquivoDataDeEmissaoDoDocumento = "DTDOCEXP";
        public const string ReferenciaDeArquivoFormacaoGrauInstrucao = "STATUSUN";
        public const string ReferenciaDeArquivoEstadoCivilDoParticipante = "CDESTCIV";
        public const string ReferenciaDeArquivoNomeMaeCliente = "NMMAECLI";
        public const string ReferenciaDeArquivoNomePaiCliente = "NMPAICLI";
        public const string ReferenciaDeArquivoCepDaResidenciaDoParticipante = "CEP";
        public const string ReferenciaDeArquivoLogradouroDaResidenciaDoParticipante = "LOGRAD";
        public const string ReferenciaDeArquivoNumeroDaResidenciaDoParticipante = "NUMLEC";
        public const string ReferenciaDeArquivoComplementoDaResidenciaDoParticipante = "COMPL";
        public const string ReferenciaDeArquivoBairroDaResidenciaDoParticipante = "BAIRRO";
        public const string ReferenciaDeArquivoCidadeDaResidenciaDoParticipante = "CIDADE";
        public const string ReferenciaDeArquivoEstadoDaResidenciaDoParticipante = "ESTADO";
        public const string ReferenciaDeArquivoValorRenda = "VRENDFAM";
        public const string ReferenciaDeArquivoNomeConjuge = "NMCONJ";
        public const string ReferenciaDeArquivoSexoDoParticipante = "CODSEX";
        public const string ReferenciaDeArquivoTipoDeDocumentoDeIdentificacao = "TPDOCIDE";
        public const string ReferenciaDeArquivoOcupacao = "IDTPPRO";
        public const string ReferenciaDeArquivoTelefoneCelular = "TELCLCLI";
        public const string ReferenciaDeArquivoEmail = "EMAILCLI";
        public const string ReferenciaDeArquivoTipoLogradouroResidencial = "CDTPRES";
        public const string ReferenciaDeArquivoValorNoImpostoRenda = "VLRCMPRND";
        public const string ReferenciaDeArquivoTipoComprovanteResidencia = "TPRESD";
        public const string ReferenciaDeArquivoAcoesVideo = "ACOESVIDEO";
        public const string ReferenciaDeArquivoValorPadrimonioImoveis = "VPTRMIMV";
        public const string ReferenciaDeArquivoValorPadrimonioVeiculos = "VPTRMVCL";
        public const string ReferenciaDeArquivoInvestimento = "IVTMTO";
        public const string ReferenciaDeArquivoIdentificacaoProposta = "IDENTIFICACAO";
        public const string ReferenciaDeArquivoDataDeValidadeDocumentoIdentificacao = "VLDID";
        public const string ReferenciaDeArquivoDataValidadeDoDocumentoResidencia = "VLDRESID";
        public const string ReferenciaDeArquivoTelefoneFixo = "TELFXCLI";
        public const string ReferenciaDeArquivoIndicadorAlteracaoDadosCadastrais = "ALTERACAO";
        public const string ReferenciaDeArquivoValorConsumoResidencial = "VLCONSRESD";
        public const string ReferenciaDeArquivoCargoDaRendaDoParticipante = "CRGORND";
        public const string ReferenciaDeArquivoClassificacaoDaRendaDoParticipante = "CLSSRND";
        public const string ReferenciaDeArquivoCompetenciaDaRendaDoParticipante = "COMPRND";
        public const string ReferenciaDeArquivoDataDaRendaDoParticipante = "DTRND";
        public const string ReferenciaDeArquivoEmpresaDaRendaDoParticipante = "EMPRRND";
        public const string ReferenciaDeArquivoOutrasOrigensDeRendaDoParticipante = "OTRORIRND";
        public const string ReferenciaDeArquivoOrigemDaRendaDoParticipante = "ORRND";
        public const string ReferenciaDeArquivoValorDaRendaDoParticipante = "VLRRND";
        public const string ReferenciaDeArquivoDataHolerite = "DTRND";
        public const string ReferenciaDeArquivoDataFichaDeCadastro = "DTRECEB";
        public const string ReferenciaDeArquivoCodigoBrSafe = "IDBRSAFE";
        public const string ReferenciaDeArquivoScoreBrSafe = "NRBRSAFE";
        public const string ReferenciaDeArquivoSiglaDoDocumentoDeOrigem = "SIGLADOC";
        public const string ReferenciaDeArquivoSituacaoDocNaBrSafe = "SITBRSAFE";
        public const string ReferenciaDeArquivoDataObito = "DTOBTO";
        public const string ReferenciaDeArquivoCobrancaTerceira = "COBRAN";
        public const string ReferenciaDeArquivoSituacaoReceita = "CPFRECEITA";
        public const string ReferenciaDeArquivoScoreCobranca = "SCOBRAN";
        public const string ReferenciaDeArquivoSigno = "SIGNO";
        public const string ReferenciaDeArquivoPpe = "PPE";
        public const string ReferenciaDeArquivoChequeSemFundo = "CHEQSEMFUND";
        public const string ReferenciaDeArquivoSignoInformado = "RESPSIGCAPT";
        public const string ReferenciaDeArquivoUfDi = "UFDI";
        public const string ReferenciaDeArquivoGraficaDeImpressao = "GRAFIMPR";
        public const string ReferenciaDeArquivoVertros = "VERTROS";
        public const string ReferenciaDeArquivoScoreEndereco = "SENDEREC";
        public const string ReferenciaDeArquivoFotoRecusada = "FTCLIRECUSA";

        public const string ReferenciaDeArquivoCep = "CEP";
        public const string ReferenciaDeArquivoEndereco = "ENDERECO";
        public const string ReferenciaDeArquivoNumeroDoRegistro = "NUREGDI";
        public const string ReferenciaDeArquivoNaturalidade = "PNATURAL";
        public const string ReferenciaDeArquivoNumeroDoDi = "IDDOCLI";
        public const string ReferenciaDeArquivoAssinaturaDi = "ASSINATURA";
        public const string ReferenciaDeArquivoLatitude = "LATITUDE";
        public const string ReferenciaDeArquivoLongitude = "LONGITUDE";

        public const int CampoPalavrasOcr = 1;
        public const int CampoCertidaoCasamentoMarido = 862;
        public const int CampoCertidaoCasamentoEsposa = 863;
        public const int CampoCnhNomePai = 776;
        public const int CampoCnhNomeMae = 777;
        public const int CampoDtEmissaoNumerica = 1591;
        public const int CampoDtNascimentoNumerica = 1592;
        public const int CampoCpfDocNaoIdentificado = 1294;
        public const int CampoNumeroComprovanteDeResidencia = 857;
        public const int CampoLogradouroComprovanteDeResidencia = 856;
        public const int CampoDataSolicitacaoAtestadoMatricula = 1425;
        public const int CampoDataValidadeAtestadoMatricula = 1566;
        public const int CampoDataEmissaoAtestadoMatricula = 1621;
        public const int CampoDataNascimento = 765;
        public const int CampoDataEmissao = 766;
        public const int CampoNomeAtestadoMatricula = 1459;
        public const int CampoComprovantePagtoCodBarras = 1648;
        public const int CampoBoletoComAutenticCodBarras = 1633;
        public const int CampoBoletoSemAutenticCodBarras = 1644;
        public const int CampoBoletoComAutenticDataEmissao = 1630;
        public const int CampoBoletoSemAutenticDataEmissao = 1641;
        
        //// Campos do documento Ficha de Cadastro
        public const int CampoCpfFichaCadastro = 1739;
        public const int CampoNomeFichaCadastro = 1738;
        public const int CampoLogradouroFichaCadastro = 1751;
        public const int CampoNumeroFichaCadastro = 1752;
        public const int CampoComplementoFichaCadastro = 1753;
        public const int CampoBairroFichaCadastro = 1754;
        public const int CampoCidadeFichaCadastro = 1755;
        public const int CampoUfFichaCadastro = 1756;
        public const int CampoCepFichaCadastro = 1757;
        public const int CampoDddFichaCadastro = 1758;
        public const int CampoTelefoneFichaCadastro = 1759;
        public const int CampoEmpresaFichaCadastro = 1760;
        public const int CampoCnpjFichaCadastro = 1761;
        public const int CampoDataAdmissaoFichaCadastro = 1762;
        public const int CampoEmpresaEnderecoFichaCadastro = 1763;
        public const int CampoEmpresaNumeroFichaCadastro = 1764;
        public const int CampoEmpresaBairroFichaCadastro = 1765;
        public const int CampoEmpresaCidadeFichaCadastro = 1766;
        public const int CampoEmpresaCepFichaCadastro = 1767;
        public const int CampoEmpresaUfFichaCadastro = 1781;
        public const int CampoEmpresaComplementoFichaCadastro = 1780;
        public const int CampoSalarioFichaCadastro = 1768;
        public const int CampoNomeReferencia1FichaCadastro = 1769;
        public const int CampoDddReferencia1FichaCadastro = 1770;
        public const int CampoTelefoneReferencia1FichaCadastro = 1771;
        public const int CampoNomeReferencia2FichaCadastro = 1772;
        public const int CampoDddReferencia2FichaCadastro = 1773;
        public const int CampoTelefoneReferencia2FichaCadastro = 1774;
        public const int CampoTipoSolicitacaoFichaCadastro = 1779;
        public const int CampoProfissaoFichaCadastro = 1806;
        public const int CampoOcupacaoFichaCadastro = 1807;
        public const int CampoFichaVirtual = 1813;
        public const int CampoDataRecebimentoPac = 2410;

        public const int CampoIndicadorAssinaturaDelegado = 1749;
        public const int CampoIndicadorFormatoDataEmissao = 1757;
        public const int CampoIndicadorGraficaImpressao = 1809;
        public const int CampoIndicadorNumeroRegistroCnh = 1751;
        public const int CampoIndicadorPopularidadeGeral = 2669;
        public const int CampoIndicadorPopularidadeDataExpedicao = 2670;
        public const int CampoIndicadorScoreCpfDataExpedicao = 2671;
        public const int CampoIndicadorScoreCpfUf = 2672;

        public const int CampoFotoBateComDi = 2673;
        
        public static readonly int[] AgenciaDaPacIds = new[] { 576, 578, 585, 504, 560 };
        public static readonly int[] ContaDaPacIds = new[] { 577, 579, 586, 505, 561 };
        public static readonly int[] CamposAssinaturaGerenteDaConta = new[] { 1003, 1086, 1178, 1254, 605, 750, 686 };
        public static readonly int[] CamposIndicadorMensalidadeMenor = new[] { 601, 683 };
        
        public Campo()
        {
            this.MappedFields = new List<MapeamentoCampo>();
        }

        /// <summary>
        /// TODO: renomear para Nome
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        public virtual string ReferenciaArquivo
        {
            get;
            set;
        }

        public virtual int Ordem
        {
            get;
            set;
        }

        public virtual List<MapeamentoCampo> MappedFields
        {
            get;
            set;
        }

        public virtual TipoDocumento TipoDocumento
        {
            get;
            set;
        }

        public virtual TipoDado TipoDado
        {
            get;
            set;
        }

        public virtual TipoCampo TipoCampo
        {
            get;
            set;
        }

        public virtual bool ParaValidacao
        {
            get;
            set;
        }

        public virtual Campo CampoParaBaterComPac
        {
            get; 
            set;
        }

        public virtual string Mascara
        {
            get;
            set;
        }

        public virtual string MascaraComplemento
        {
            get;
            set;
        }

        public virtual GrupoCampo Grupo 
        { 
            get;
            set;
        }

        public virtual int OrdemNoGrupo
        {
            get;
            set;
        }

        public virtual bool DuplaDigitacao
        {
            get; 
            set;
        }

        public virtual bool Digitavel
        {
            get;
            set;
        }

        public virtual bool Obrigatorio
        {
            get;
            set;
        }

        public virtual bool Indexador
        {
            get;
            set;
        }

        public virtual bool Reconhecivel
        {
            get;
            set;
        }

        public virtual bool OcrComplementou
        {
            get;
            set;
        }

        public virtual bool PodeInserirPeloOcr
        {
            get;
            set;
        }

        public virtual string StyleNoGrupo
        {
            get;
            set;
        }

        public virtual string TabelaDinamica()
        {
            return this.ObterItemDoComplemento("TabelaDinamica");
        }

        public virtual bool EstaMapeadoPara(string field, string templateName)
        {
            return this.MappedFields.Any(map => 
                    map.NomeCampoNoTemplate.ToUpper() == field.ToUpper() && 
                    map.NomeTemplate.ToUpper() == templateName.ToUpper());
        }

        public virtual void AddFieldMapping(MapeamentoCampo mapeamentoCampo)
        {
            mapeamentoCampo.Campo = this;
            this.MappedFields.Add(mapeamentoCampo);
        }

        private string ObterItemDoComplemento(string chave)
        {
            if (string.IsNullOrEmpty(this.MascaraComplemento))
            {
                return string.Empty;
            }

            var parametros = this.MascaraComplemento.Split(';');

            foreach (var parametro in parametros)
            {
                var itens = parametro.Split('=');

                if (itens[0].ToUpper() == chave.ToUpper())
                {
                    return itens[1];
                }
            }

            return string.Empty;
        }
    }
}
