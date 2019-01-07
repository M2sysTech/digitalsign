namespace Veros.Paperless.Model
{
    using System;
    using System.Configuration;
    using System.IO;

    public class Configuracao
    {
        public static string CaminhoDeImportacaoDeImagens
        {
            get
            {
                return Path.Combine(CaminhoDePacotesRecebidos, "Desempacotado");
            }
        }

        public static string CaminhoDeImportacaoDeIndice
        {
            get
            {
                return Path.Combine(CaminhoDePacotesRecebidos, "Desempacotado");
            }
        }

        public static string CaminhoDeIndiceJaProcessado
        {
            get
            {
                return Path.Combine(CaminhoDeImportacaoDeIndice, "ProcessamentoConcluido");
            }
        }

        public static string CaminhoDePacotesRecebidos
        {
            get
            {
                return ConfigurationManager.AppSettings["Recepcao.Diretorio.Pacotes"];
            }
        }

        public static string CaminhoDeArquivosExportacao
        {
            get
            {
                return ConfigurationManager.AppSettings["Exportacao.Diretorio"];
            }
        }

        public static string CaminhoDeFotoDeUsuario
        {
            get
            {
                return Path.Combine(CaminhoDePacotesRecebidos, "Fotos");
            }
        }

        public static TimeSpan JanelaDeSla 
        { 
            get
            {
                var dataMaior = DateTime.Now;
                var dataMenor = dataMaior.AddHours(-2);
                return dataMaior - dataMenor;
            }
        }

        public static string LicencaEngineReconhecimento
        {
            get
            {
                return ConfigurationManager.AppSettings["Recognize.Licenca.Abbyy"];
            }
        }
    }
}