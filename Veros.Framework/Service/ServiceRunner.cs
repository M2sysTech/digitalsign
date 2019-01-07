namespace Veros.Framework.Service
{
    using System;
    using System.ServiceProcess;

    public class ServiceRunner
    {
        private readonly IWindowsService windowsService;

        public ServiceRunner(IWindowsService windowsService)
        {
            this.windowsService = windowsService;
        }

        public void Run(string[] args)
        {
            if (Aplicacao.EstaRodandoComoServico)
            {
                ServiceBase.Run(new SimpleWindowsService(this.windowsService));
            }
            else
            {
                this.windowsService.Start();
                Console.ReadLine();
                this.windowsService.Stop();
            }
        }
    }
}