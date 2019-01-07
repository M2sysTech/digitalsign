namespace Veros.Paperless.Infra
{
    using System.Collections.Generic;
    using System;
    using Model.Entidades;

    public static class ContextoInfra
    {
        public static TimeSpan IntervaloDeCadaPedidoParaFila = TimeSpan.FromSeconds(60);
        
        public static int Nucleos
        {
            get
            {
#if DEBUG
                //// return Aplicacao.Nucleos;
                return 1;
#else
                return Veros.Framework.Aplicacao.Nucleos;
#endif
            }
        }

        ////public static RedisConnection RedisClientManager
        ////{
        ////    get;
        ////    set;
        ////}

        public static int MinimoPalavrasPaginaBranca
        {
            get;
            set;
        }

        public static bool ExcluirPaginasBrancas
        {
            get;
            set;
        }

        public static ConfiguracaoIp FilaOcr
        {
            get;
            set;
        }

        public static ConfiguracaoIp MonitorOcr
        {
            get;
            set;
        }

        public static ConfiguracaoIp FileTransfer
        {
            get;
            set;
        }

        public static ConfiguracaoIp Redis
        {
            get;
            set;
        }

        public static ConfiguracaoIp MonitorServicos
        {
            get;
            set;
        }

        public static ConfiguracaoIp Fila
        {
            get;
            set;
        }

        public static bool AdicinarPontoFinalNoComplementoDeEndereco
        {
            get;
            set;
        }

        public static bool DeveAutenticarProxy 
        { 
            get; 
            set; 
        }

        public static string ProxyUri
        {
            get; 
            set;
        }

        public static string ProxyUser
        {
            get; 
            set;
        }

        public static string ProxyPassword
        {
            get; 
            set;
        }

        public static string ProxyDominio
        {
            get; 
            set;
        }

        public static bool IsTtScan
        {
            get; 
            set;
        }

        public static string EmailHost
        {
            get;
            set;
        }

        public static int EmailPorta
        {
            get;
            set;
        }

        public static string EmailUsuario
        {
            get;
            set;
        }

        public static string EmailSenha
        {
            get;
            set;
        }

        public static bool AssinaturaDigitalAtivada
        {
            get;
            set;
        }

        public static bool AssinaturaDigitalVisivel
        {
            get;
            set;
        }

        public static string ConsultaPessoaPh3Url
        {
            get; 
            set;
        }

        public static string AutenticacaoPh3Url
        {
            get; 
            set;
        }

        public static string DocumentoscopiaUrl
        {
            get; 
            set;
        }

        public static string VertrosUrl
        {
            get; 
            set;
        }

        public static string VertrosUsuario
        {
            get; 
            set;
        }

        public static string VertrosSenha
        {
            get; 
            set;
        }

        public static string VertrosApiKey
        {
            get; 
            set;
        }

        public static float IndiceCorteBiometria
        {
            get; 
            set;
        }

        public static float IndiceSelfieVersusDi
        {
            get;
            set;
        }

        public static int FaceThresholdLuxand
        {
            get;
            set;
        }

        public static float LimiteFaceComumEncontrada
        {
            get; 
            set;
        }

        public static string AntiFraudePortalUrl
        {
            get;
            set;
        }

        public static int PosicaoCertificado
        {
            get;
            set;
        }

        public static string EmailSuportePara
        {
            get;
            set;
        }

        public static string AmazonRegion
        {
            get;
            set;
        }

        public static string AmazonBucketName
        {
            get;
            set;
        }

        public static string AmazonAccessKey
        {
            get;
            set;
        }

        public static string AmazonSecretKey
        {
            get;
            set;
        }

        public static string AmazonEntryPointUrl
        {
            get;
            set;
        }
    }
}
