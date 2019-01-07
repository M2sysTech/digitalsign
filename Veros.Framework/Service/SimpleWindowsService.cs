//-----------------------------------------------------------------------
// <copyright file="SimpleWindowsService.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Service
{
    using System;
    using System.ServiceProcess;

    public class SimpleWindowsService : ServiceBase
    {
        private readonly IWindowsService service;

        public SimpleWindowsService(IWindowsService service)
        {
            this.service = service;

            this.CanStop = true;
            this.CanPauseAndContinue = false;
            this.AutoLog = true;
        }
        
        protected override void OnStart(string[] args)
        {
            try
            {
                this.service.Start();
                Log.Application.Info("Serviço iniciado");
            }
            catch (Exception exception)
            {
                Log.Application.Error(exception);
            }
        }

        protected override void OnStop()
        {
            try
            {
                this.service.Stop();
                Log.Application.Info("Serviço parado");
            }
            catch (Exception exception)
            {
                Log.Application.Error(exception);
            }
        }
    }
}