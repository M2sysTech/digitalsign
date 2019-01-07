namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseProcessoExportado : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoExportado()
        {
            this.FaseEstaAtiva = x => x.ExportacaoAtiva;
            this.StatusDaFase = ProcessoStatus.ExportacaoRealizada;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Finalizado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.Faturamento;
            processo.Lote.DataFimExportacao = DateTime.Now;
        }
    }
}
