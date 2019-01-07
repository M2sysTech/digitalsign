namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseProcessoEnviado : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoEnviado()
        {
            this.FaseEstaAtiva = x => x.EnvioAtivo;
            this.StatusDaFase = ProcessoStatus.EnvioRealizado;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.SetaRetorno;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.SetaRetorno;
            processo.Lote.DataFimEnvio = DateTime.Now;
        }
    }
}
