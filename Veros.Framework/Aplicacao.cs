namespace Veros.Framework
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Security.Principal;
    using static System.Net.Mime.MediaTypeNames;

    public class Aplicacao
    {
        public static string Caminho
        {
            get;
            private set;
        }

        public static bool Rodando64Bits
        {
            get { return Environment.Is64BitProcess; }
        }

        public static string Nome
        {
            get
            {
                var entryAssembly = Assembly.GetEntryAssembly();
                
                if (entryAssembly == null)
                {
                    return AppDomain.CurrentDomain.FriendlyName.Replace(' ', '_').Replace(':', '_');
                }

                return entryAssembly.GetName().Name;
            }
        }

        public static string NomeDaMaquina
        {
            get
            {
                return Environment.MachineName;
            }
        }

        public static bool EstaRodandoComoServico
        {
            get
            {
                return Environment.UserInteractive == false;
            }
        }

        public static int Nucleos
        {
            get
            {
                return Environment.ProcessorCount;
            }
        }

        public static string Versao
        {
            get
            {
                return "1.0.0.0"; //// Application.ProductVersion;
            }
        }

        public static Assembly MainAssembly
        {
            get;
            internal set;
        }

        internal static bool EstaRodandoComoTesteAutomatizado
        {
            get;
            set;
        }

        public static string ObterPastaVeros()
        {
            return Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Veros");
        }

        public static void Configurar(string caminhoDaAplicacao)
        {
            if (string.IsNullOrEmpty(caminhoDaAplicacao))
            {
                caminhoDaAplicacao = AppDomain.CurrentDomain.BaseDirectory;
            }

            Caminho = caminhoDaAplicacao;
        }

        public static bool EstaRodandoComoAdministrador()
        {
            try
            {
                var user = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(user);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}