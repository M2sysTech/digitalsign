namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseProcessoRetornoFinalizado : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoRetornoFinalizado()
        {
            this.FaseEstaAtiva = x => x.RetornoAtivo;
            this.StatusDaFase = ProcessoStatus.RetornoFinalizado;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Finalizado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.Finalizado;
        }
    }
}
