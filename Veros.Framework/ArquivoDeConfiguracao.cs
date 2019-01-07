namespace Veros.Framework
{
    using System.Configuration;

    public class ArquivoDeConfiguracao : IArquivoDeConfiguracao
    {
        public string Obter(string nome)
        {
            return ConfigurationManager.AppSettings[nome];
        }
    }
}