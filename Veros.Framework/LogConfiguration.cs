namespace Veros.Framework
{
    using log4net.Core;

    public class LogConfiguration
    {
        public LogConfiguration()
        {
            this.ConsoleAtivo = false;
            this.ArquivoAtivo = false;
            this.Level = Level.Error;
            this.SqlAtivo = false;
            this.ConsolePattern = "%message %newline";
            ////this.ConsolePattern = "[%5.5thread] %message %newline";
            this.NomeDoArquivo = Aplicacao.Nome;
        }

        public bool ConsoleAtivo
        {
            get;
            set;
        }

        public bool TraceAtivo
        {
            get;
            set;
        }

        public string ConsolePattern
        {
            get;
            set;
        }

        public bool ArquivoAtivo
        {
            get;
            set;
        }

        public bool SqlAtivo
        {
            get;
            set;
        }

        public Level Level
        {
            get;
            set;
        }

        public string NomeDoArquivo
        {
            get;
            set;
        }

        public void Configurar()
        {
            this.ConsoleAtivo = SettingsConfig.LogConsole;
            this.ArquivoAtivo = SettingsConfig.LogFile;
            this.SqlAtivo = SettingsConfig.LogSql;
            this.Level = SettingsConfig.LogLevel;
            this.ConsolePattern = SettingsConfig.LogConsolePattern;
            this.NomeDoArquivo = Aplicacao.Nome;
        }

        public LogConfiguration MergeWithSettingsConfig()
        {
            this.ConsoleAtivo = this.ConsoleAtivo || SettingsConfig.LogConsole;
            this.TraceAtivo = this.TraceAtivo || SettingsConfig.LogTrace;
            this.ArquivoAtivo = this.ArquivoAtivo || SettingsConfig.LogFile;
            this.SqlAtivo = this.SqlAtivo || SettingsConfig.LogSql;
            this.Level = SettingsConfig.LogLevel <= this.Level ? SettingsConfig.LogLevel : this.Level;
            this.ConsolePattern = this.ConsolePattern;

            return this;
        }
    }
}