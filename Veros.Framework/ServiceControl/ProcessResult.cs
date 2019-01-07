namespace Veros.Framework.ServiceControl
{
    using System.Diagnostics;

    public class ProcessResult
    {
        public ProcessResult(Process process)
        {
            this.ExitCode = process.ExitCode;
            this.Output = process.StandardOutput.ReadToEnd();
            this.Error = process.StandardError.ReadToEnd();
        }

        public string Error
        {
            get;
            private set;
        }

        public string Output
        {
            get;
            private set;
        }

        public int ExitCode
        {
            get;
            private set;
        }
    }
}