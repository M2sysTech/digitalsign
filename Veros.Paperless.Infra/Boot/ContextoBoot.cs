namespace Veros.Paperless.Infra.Boot
{
    using System;
    using System.Linq;
    using Data;
    using Filas;
    using Framework;
    using Framework.Service;
    using Model;
    using Model.Entidades;
    using Model.Repositorios;

    public class ContextoBoot : IApplicationBoot
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguracaoIpRepositorio configuracaoIpRepositorio;
        private readonly ITagRepositorio tagRepositorio;

        public ContextoBoot(
            IUnitOfWork unitOfWork, 
            IConfiguracaoIpRepositorio configuracaoIpRepositorio, 
            ITagRepositorio tagRepositorio)
        {
            this.unitOfWork = unitOfWork;
            this.configuracaoIpRepositorio = configuracaoIpRepositorio;
            this.tagRepositorio = tagRepositorio;
        }

        public void Execute()
        {
            if (ContextoInfra.IsTtScan)
            {
                return;
            }

            var connectionString = Database.ConnectionString;

#if DEBUG == false
            if (connectionString.ToLower().Contains("gedcef") == false)
            {
                Log.Application.Error(
                    "Você não está usando um banco de dados gedcef. Informe uma base do gedcef na string de conexão no arquivo settings.config da pasta c:\\veros\\paperles\\config e execute o comando deploy config no console");

                Environment.Exit(0);
            } 
#endif
            //// obtem configuracoes de ip
            this.unitOfWork.Transacionar(() =>
            {
                this.ConfiguracoesTratamentoImagem();

                this.ConfiguracoesIniciaisDoAmbiente();

                this.ConfiguracoesIniciaisClassifier();

                this.ConfiguracoesDimensoesBarcodeSeparador();

                this.CarregarInformacoesProxy();

                this.CarregarInformacoesEmail();

                this.ConfigurarTagsIntegracao();

                this.ConfigurarTagsAssinaturaDigital();

                this.ConfigurarPortalVertros();

                this.ConfigurarPosicaoCertificadoCartao();

                this.ConfigurarInformacoesAmazonStorage();

                this.CarregarTempoFilasWeb();
            });

            //// configura pools
            FilaOcrCliente.Pool = new FilaOcrClientePool(
                ContextoInfra.Nucleos, ContextoInfra.FilaOcr);

            FilaComparacaoCliente.Pool =  
                new FilaComparacaoClientePool(ContextoInfra.Nucleos, ContextoInfra.Fila);

            FilaFaceExtractorCliente.Pool =
                new FilaFaceExtractorClientePool(ContextoInfra.Nucleos, ContextoInfra.Fila);

            FilaSelfieDiCliente.Pool =
                new FilaSelfieDiClientePool(ContextoInfra.Nucleos, ContextoInfra.Fila);
        }

        private void ConfigurarInformacoesAmazonStorage()
        {
            ContextoInfra.AmazonAccessKey = this.tagRepositorio.ObterValorPorNome("storage.amazon.accesskey", "AKIAJ7VXUYYJX5CTHE6Q");
            ContextoInfra.AmazonSecretKey = this.tagRepositorio.ObterValorPorNome("storage.amazon.secretkey", "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");
            ContextoInfra.AmazonRegion = this.tagRepositorio.ObterValorPorNome("storage.amazon.region", "sa-east-1");
            ContextoInfra.AmazonBucketName = this.tagRepositorio.ObterValorPorNome("storage.amazon.bucket", "ftcef01");
            ContextoInfra.AmazonEntryPointUrl = this.tagRepositorio.ObterValorPorNome("storage.amazon.entrypoint", "https://{0}.s3{1}-accelerate.amazonaws.com/{2}/{3}");
        }

        private void ConfiguracoesIniciaisDoAmbiente()
        {
            ContextoInfra.FilaOcr = this.configuracaoIpRepositorio.ObterPorTag("QUEUEOCR");

            ContextoInfra.Fila = this.configuracaoIpRepositorio.ObterPorTag("QUEUE");

            ContextoInfra.MonitorOcr = this.configuracaoIpRepositorio.ObterPorTag("MONOCR");

            ContextoInfra.FileTransfer = this.configuracaoIpRepositorio.ObterPorTag("FILE");

            ContextoInfra.Redis = this.configuracaoIpRepositorio.ObterPorTag("REDIS");

            ContextoInfra.MonitorServicos = this.configuracaoIpRepositorio.ObterPorTag("MONSRV");

            var tagAdicinarPonto = this.tagRepositorio.ObterPorNome("AD_PONTO_NO_COMPLEMENTO");

            if (tagAdicinarPonto != null)
            {
                ContextoInfra.AdicinarPontoFinalNoComplementoDeEndereco = tagAdicinarPonto.Valor == "TRUE";
            }

            if (ContextoInfra.Redis == null)
            {
                ContextoInfra.Redis = new ConfiguracaoIp
                {
                    Host = "localhost"
                };
            }

            ContextoInfra.EmailSuportePara = this.tagRepositorio.ObterValorPorNome("alarme.email.para", "monitoramento@m2sys.com.br");

            var tagCorteFaceComum = this.tagRepositorio.ObterValorPorNome("COMPARATOR_CORTE_FACECOMUM", "10");
            ContextoInfra.LimiteFaceComumEncontrada = tagCorteFaceComum.ToInt();

            var tagCorteBiometria = this.tagRepositorio.ObterValorPorNome("CLASSIF_CORTE_BIOMETRIA", "0,51");
            ContextoInfra.IndiceCorteBiometria = float.Parse(tagCorteBiometria);

            var tagFraudeUrl = this.tagRepositorio.ObterPorNome("pipelog.resturl.antifraude");

            if (tagFraudeUrl != null)
            {
                ContextoInfra.AntiFraudePortalUrl = tagFraudeUrl.Valor;
            }
        }

        private void ConfiguracoesDimensoesBarcodeSeparador()
        {
            var rangeX = this.tagRepositorio
                .ObterValorPorNome("separador.barcode.range.x", "600-800");

            var rangeY = this.tagRepositorio
                .ObterValorPorNome("separador.barcode.range.y", "1050-1600");

            var rangeWidth = this.tagRepositorio
                .ObterValorPorNome("separador.barcode.range.width", "500-800");

            var rangeHeight = this.tagRepositorio
                .ObterValorPorNome("separador.barcode.range.height", "200-500");

            ////DimensaoSeparador.MinimoX = decimal.Parse(rangeX.Split(new[] { "-" }, StringSplitOptions.None).First());
            ////DimensaoSeparador.MaximoX = decimal.Parse(rangeX.Split(new[] { "-" }, StringSplitOptions.None).Last());

            ////DimensaoSeparador.MinimoY = decimal.Parse(rangeY.Split(new[] { "-" }, StringSplitOptions.None).First());
            ////DimensaoSeparador.MaximoY = decimal.Parse(rangeY.Split(new[] { "-" }, StringSplitOptions.None).Last());

            ////DimensaoSeparador.MinimoLargura = decimal.Parse(rangeWidth.Split(new[] { "-" }, StringSplitOptions.None).First());
            ////DimensaoSeparador.MaximoLargura = decimal.Parse(rangeWidth.Split(new[] { "-" }, StringSplitOptions.None).Last());

            ////DimensaoSeparador.MinimoAltura = decimal.Parse(rangeHeight.Split(new[] { "-" }, StringSplitOptions.None).First());
            ////DimensaoSeparador.MaximoAltura = decimal.Parse(rangeHeight.Split(new[] { "-" }, StringSplitOptions.None).Last());
        }

        private void ConfiguracoesTratamentoImagem()
        {
            Contexto.MinWidthPixel = this.tagRepositorio
                .ObterValorPorNome("imagem.branco.minwidthpixel", "20").ToInt();

            Contexto.MinHeightPixel = this.tagRepositorio
                .ObterValorPorNome("imagem.branco.minheightpixel", "15").ToInt();

            Contexto.MinMargemPixel = this.tagRepositorio
                .ObterValorPorNome("imagem.branco.minmargempixel", "150").ToInt();
        }

        private void ConfiguracoesIniciaisClassifier()
        {
            var valorTag = this.tagRepositorio.ObterValorPorNome("EXCLUIR_PAG_BRANCA", "FALSE");
            ContextoInfra.ExcluirPaginasBrancas = valorTag.ToUpper() == "TRUE";

            valorTag = this.tagRepositorio.ObterValorPorNome("MINIMO_PAL_PAG_BRANCA", "30");
            ContextoInfra.MinimoPalavrasPaginaBranca = valorTag.ToInt();

            valorTag = this.tagRepositorio.ObterValorPorNome("assinaturadigital.visivel", "FALSE");
            ContextoInfra.AssinaturaDigitalVisivel = valorTag.ToUpper() == "TRUE";
        }

        private void ConfigurarPosicaoCertificadoCartao()
        {
            ContextoInfra.PosicaoCertificado = this.tagRepositorio
                .ObterValorPorNome("assinaturadigital.posicaocertificado", "0")
                .ToInt();
        }

        private void ConfigurarTagsAssinaturaDigital()
        {
            Contexto.AssinaturaDigitalAuthor = this.tagRepositorio.ObterValorPorNome("assinaturadigital.author");
            Contexto.AssinaturaDigitalCreator = this.tagRepositorio.ObterValorPorNome("assinaturadigital.creator");
            Contexto.AssinaturaDigitalKeywords = this.tagRepositorio.ObterValorPorNome("assinaturadigital.keywords");
            Contexto.AssinaturaDigitalProducer = this.tagRepositorio.ObterValorPorNome("assinaturadigital.producer");
            Contexto.AssinaturaDigitalContact = this.tagRepositorio.ObterValorPorNome("assinaturadigital.signaturecontact");
            Contexto.AssinaturaDigitalLocation = this.tagRepositorio.ObterValorPorNome("assinaturadigital.signaturelocation");
            Contexto.AssinaturaDigitalReason = this.tagRepositorio.ObterValorPorNome("assinaturadigital.signaturereason");
            Contexto.AssinaturaDigitalSubject = this.tagRepositorio.ObterValorPorNome("assinaturadigital.subject");
            Contexto.AssinaturaDigitalTitle = this.tagRepositorio.ObterValorPorNome("assinaturadigital.title");

            var valorTag = this.tagRepositorio.ObterValorPorNome("assinaturadigital.ativada", "TRUE");
            Contexto.AssinaturaDigitalAtivada = valorTag.ToUpper() == "TRUE";
        }

        private void ConfigurarTagsIntegracao()
        {
            var tagConsultaPh3 = this.tagRepositorio.ObterPorNome("consulta.ph3a.url");
            var tagAutenticacaoPh3 = this.tagRepositorio.ObterPorNome("autenticacao.ph3.url");

            if (tagConsultaPh3 != null)
            {
                ContextoInfra.ConsultaPessoaPh3Url = tagConsultaPh3.Valor;
            }

            if (tagAutenticacaoPh3 != null)
            {
                ContextoInfra.AutenticacaoPh3Url = tagAutenticacaoPh3.Valor;
            }

            var tagDocumentoscopiaUrl = this.tagRepositorio.ObterPorNome("documentoscopia.url");

            if (tagDocumentoscopiaUrl != null)
            {
                ContextoInfra.DocumentoscopiaUrl = tagDocumentoscopiaUrl.Valor;
            }
        }

        private void ConfigurarPortalVertros()
        {
            var tagVertros = this.tagRepositorio.ObterPorNome("vertros.url");
            var tagVertrosUsuario = this.tagRepositorio.ObterPorNome("vertros.usuario");
            var tagVertrosSenha = this.tagRepositorio.ObterPorNome("vertros.senha");
            var tagVertrosApiKey = this.tagRepositorio.ObterPorNome("vertros.apikey");

            if (tagVertros != null)
            {
                ContextoInfra.VertrosUrl = tagVertros.Valor;
            }

            if (tagVertrosUsuario != null)
            {
                ContextoInfra.VertrosUsuario = tagVertrosUsuario.Valor;
            }

            if (tagVertrosSenha != null)
            {
                ContextoInfra.VertrosSenha = tagVertrosSenha.Valor;
            }

            if (tagVertrosApiKey != null)
            {
                ContextoInfra.VertrosApiKey = tagVertrosApiKey.Valor;
            }
        }

        private void CarregarInformacoesProxy()
        {
            if (this.tagRepositorio.ObterPorNome("consulta.proxy.autenticar") == null)
            {
                return;
            }

            ContextoInfra.DeveAutenticarProxy = this.tagRepositorio.ObterPorNome("consulta.proxy.autenticar").Valor == "true";
            ContextoInfra.ProxyUri = this.tagRepositorio.ObterPorNome("consulta.proxy.uri").Valor;
            ContextoInfra.ProxyUser = this.tagRepositorio.ObterPorNome("consulta.proxy.user").Valor;
            ContextoInfra.ProxyPassword = this.tagRepositorio.ObterPorNome("consulta.proxy.password").Valor;
            ContextoInfra.ProxyDominio = this.tagRepositorio.ObterPorNome("consulta.proxy.dominio").Valor;
        }

        private void CarregarTempoFilasWeb()
        {
            Contexto.TempoFilasWeb = this.tagRepositorio.ObterValorPorNome("tempo.espera.filas.web", "5").ToLower().ToInt();
        }

        private void CarregarInformacoesEmail()
        {
            if (this.tagRepositorio.ObterPorNome("sender.mail.host") == null)
            {
                return;
            }

            ContextoInfra.EmailHost = this.tagRepositorio.ObterPorNome("sender.mail.host").Valor;
            ContextoInfra.EmailPorta = this.tagRepositorio.ObterPorNome("sender.mail.porta").Valor.ToInt();
            ContextoInfra.EmailUsuario = this.tagRepositorio.ObterPorNome("sender.mail.usuario").Valor;
            ContextoInfra.EmailSenha = this.tagRepositorio.ObterPorNome("sender.mail.senha").Valor;
        }
    }
}
