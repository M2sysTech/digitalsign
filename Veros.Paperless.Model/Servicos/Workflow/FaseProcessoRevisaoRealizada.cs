namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseProcessoRevisaoRealizada : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoRevisaoRealizada()
        {
            this.FaseEstaAtiva = x => x.FormalisticaAtiva;
            this.StatusDaFase = ProcessoStatus.RevisaoRealizada;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.SetaAprovacao;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.SetaAprovacao;
            processo.AlterarStatusDosDocumentos(DocumentoStatus.RevisaoRealizada);
            processo.Lote.DataFimRevisao = DateTime.Now;
        }
    }
}