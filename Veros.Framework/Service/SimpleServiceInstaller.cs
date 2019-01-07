//-----------------------------------------------------------------------
// <copyright file="SimpleServiceInstaller.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Service
{
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.ServiceProcess;

    [RunInstaller(true)]
    public abstract class SimpleServiceInstaller : Installer
    {
        private readonly ServiceProcessInstaller serviceProcessInstaller1;
        private readonly ServiceInstaller serviceInstaller1;

        public SimpleServiceInstaller()
        {
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;

            this.GetWindowsService();
            //// TODO: repensar instalacao de servico
            ////this.serviceInstaller1.DisplayName = this.windowsService.DisplayName;
            ////this.serviceInstaller1.ServiceName = this.windowsService.ServiceName;
            ////this.serviceInstaller1.Description = this.windowsService.Description;
            
            this.Installers.AddRange(new System.Configuration.Install.Installer[] 
            {
                this.serviceProcessInstaller1,
                this.serviceInstaller1
            });
        }

        public abstract IWindowsService GetWindowsService();
    }
}
