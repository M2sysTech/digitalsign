namespace Veros.Framework
{
    using System.Configuration;

    public class SettingsConfig
    {
        public static log4net.Core.Level LogLevel
        {
            get
            {
                var nivel = Obter("Log.Level", "error").ToLower();

                if (nivel.Equals("info"))
                {
                    return log4net.Core.Level.Info;
                }

                if (nivel.Equals("debug"))
                {
                    return log4net.Core.Level.Debug;
                }

                return log4net.Core.Level.Error;
            }
        }

        public static bool LogFile
        {
            get
            {
                return Obter("Log.File", "true").ToBoolean();
            }
        }

        public static bool LogConsole
        {
            get
            {
                return Obter("Log.Console", "false").ToBoolean();
            }
        }

        public static bool LogTrace
        {
            get
            {
                return Obter("Log.Trace", "false").ToBoolean();
            }
        }

        public static string LogConsolePattern
        {
            get
            {
                return Obter("Log.Console.Pattern");
            }
        }

        public static bool LogSql
        {
            get
            {
                return Obter("Log.Sql", "false").ToBoolean();
            }
        }

        public static string[] DependencyPrefixes
        {
            get
            {
                var prefixos = Obter("Dependency.Prefixes");
                
                if (string.IsNullOrEmpty(prefixos) == false)
                {
                    return prefixos.SepararPor(",");
                }

                return new string[] { };
            }
        }

        public static string DependencyPlugin
        {
            get
            {
                return Obter("Dependency.Plugin");
            }
        }

        public static bool ProfilerEnabled
        {
            get
            {
                return Obter("Profiler.Enabled", "false").ToBoolean();
            }
        }

        public static bool DatabaseCache
        {
            get
            {
                return Obter("Database.Cache", "false").ToBoolean();
            }
        }

        public static string DatabaseCacheServer
        {
            get
            {
                return Obter("Database.Cache.Server", "false");
            }
        }

        public static bool SeedExecute
        {
            get
            {
                return Obter("Seed.Execute", "false").ToBoolean();
            }
        }
        
        public static string Obter(string chave, string padrao = "")
        {
            var valor = ConfigurationManager.AppSettings[chave];

            if (string.IsNullOrEmpty(valor))
            {
                return padrao;
            }

            return valor;
        }
    }
}