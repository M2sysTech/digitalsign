namespace DigitalSign
{
    using Jobs;
    using System;
    using Veros.Data;
    using Veros.Data.Jobs;
    using Veros.Framework;
    using Veros.Framework.Service;
    using Veros.Paperless.Model;
    using Veros.Paperless.Model.Repositorios;

    public class WindowsService : IWindowsService
    {
        private readonly Agendamento<SignJob> signJob;
        private readonly Agendamento<SignErrorJob> signErrorJob;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITagRepositorio tagRepositorio;

        public WindowsService(IUnitOfWork unitOfWork, ITagRepositorio tagRepositorio)
        {
            this.unitOfWork = unitOfWork;
            this.tagRepositorio = tagRepositorio;

            Agendamentos.Configurar(1);
            
            this.signJob = new Agendamento<SignJob>(TimeSpan.FromSeconds(5));
            this.signErrorJob = new Agendamento<SignErrorJob>(TimeSpan.FromSeconds(5));
        }

        public void Start()
        {
            using (this.unitOfWork.Begin())
            {
                Contexto.TempoParaAssinarDocumentosComErro = this.tagRepositorio
                    .ObterValorPorNome("assinaturadigital.nova.tentativa", "3600").ToInt();
            }

            this.signJob.Start();
            ////this.signErrorJob.Start();
        }

        public void Stop()
        {
            Agendamentos.PararTodos();
        }
    }
}
