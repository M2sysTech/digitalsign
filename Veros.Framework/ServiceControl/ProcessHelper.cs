namespace Veros.Framework.ServiceControl
{
    using System.Diagnostics;

    public class ProcessHelper
    {
        public static ProcessResult ExecuteProcess(string file, string argumentsFormat, params object[] args)
        {
            return InternalExecuteProcess(false, file, argumentsFormat, args);
        }

        public static ProcessResult ExecuteProcessAndWait(string file, string argumentsFormat, params object[] args)
        {
            return InternalExecuteProcess(true, file, argumentsFormat, args);
        }

        public static void OpenProcess(string file, string arguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = file,
                    Arguments = arguments,
                    WindowStyle = ProcessWindowStyle.Maximized,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false
                }
            };

            process.Start();
        }

        private static ProcessResult InternalExecuteProcess(
            bool wait, 
            string file, 
            string argumentsFormat, 
            params object[] args)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = file,
                    Arguments = string.Format(argumentsFormat, args),
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            try
            {
                process.Start();

                if (wait)
                {
                    process.WaitForExit(15000);
                }

                return new ProcessResult(process);
            }
            finally
            {
                process.Close();
                process.Dispose();
            }
        }
    }
}