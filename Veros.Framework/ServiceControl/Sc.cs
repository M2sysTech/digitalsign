namespace Veros.Framework.ServiceControl
{
    /// <summary>
    /// TODO: mover pro framework
    /// </summary>
    public class Sc
    {
        public static bool CleanAllUses()
        {
            var result = ProcessHelper.ExecuteProcessAndWait(
                "net",
                @"use * /del /yes");

            return result.ExitCode == 0;
        }

        public static bool UseList()
        {
            var result = ProcessHelper.ExecuteProcessAndWait(
                "net",
                @"use");

            if (result.ExitCode == 2)
            {
                if (result.Error.Contains("Erro de sistema 1219") || result.Error.Contains("System error 121"))
                {
                    return true;
                }
            }

            return result.ExitCode == 0;
        }

        public static bool Use(string machine, string user, string password)
        {
            var result = ProcessHelper.ExecuteProcessAndWait(
                "net",
                @"use \\{0}\IPC$ {1} /USER:{2}",
                machine,
                password,
                user);

            if (result.ExitCode == 2)
            {
                if (result.Error.Contains("Erro de sistema 1219") || result.Error.Contains("System error 121"))
                {
                    return true;
                }
            }

            return result.ExitCode == 0;
        }

        public static bool Stop(string machine, string service)
        {
            var result = ProcessHelper.ExecuteProcessAndWait(
                "sc",
                @"\\{0} stop ""{1}""",
                machine,
                service);

            return result.ExitCode == 0 || result.ExitCode == 1062;
        }

        public static bool Start(string machine, string service)
        {
            return ProcessHelper.ExecuteProcessAndWait(
                "sc",
                @"\\{0} start ""{1}""",
                machine,
                service).ExitCode == 0;
        }

        public static bool Uninstall(string machine, string service)
        {
            return ProcessHelper.ExecuteProcessAndWait(
                "sc",
                @"\\{0} delete ""{1}""",
                machine,
                service).ExitCode == 0;
        }

        public static bool Install(
            string machine, string service, string executable, string name, string startParameters = "auto")
        {
            return ProcessHelper.ExecuteProcessAndWait(
                "sc",
                @"\\{0} create ""{1}"" binPath= ""{2}"" DisplayName= ""{3}"" start= {4}",
                machine,
                service,
                executable,
                name,
                startParameters).ExitCode == 0;
        }

        public static bool InstallWithUserLogon(
            string service, string executable, string name, string user, string password, string startParameters = "auto")
        {
            return ProcessHelper.ExecuteProcessAndWait(
                    "sc",
                    @"create ""{0}"" binPath= ""{1}"" DisplayName= ""{2}"" obj= ""{3}"" password= ""{4}"" start= {5}",
                    service,
                    executable,
                    name,
                    user,
                    password,
                    startParameters).ExitCode == 0;
        }

        public static StatusDeServico Status(string machine, string service)
        {
            var result = ProcessHelper.ExecuteProcessAndWait(
                "sc",
                @"\\{0} query ""{1}""",
                machine,
                service);

            if (result.Output.Contains("1  STOPPED"))
            {
                return StatusDeServico.Parado;
            }

            if (result.Output.Contains("3  STOP_PENDING"))
            {
                return StatusDeServico.Parando;
            }

            if (result.Output.Contains("2  START_PENDING"))
            {
                return StatusDeServico.Iniciando;
            }

            if (result.Output.Contains("4  RUNNING"))
            {
                return StatusDeServico.Iniciado;
            }

            if (result.Output.Contains("FALHA 1060") || result.Output.Contains("FAILED 1060"))
            {
                return StatusDeServico.NaoInstalado;
            }

            return StatusDeServico.Desconhecido;
        }
    }
}