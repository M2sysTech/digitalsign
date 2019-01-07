namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseProcessoFormalisticaRealizada : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoFormalisticaRealizada()
        {
            this.FaseEstaAtiva = x => x.FormalisticaAtiva;
            this.StatusDaFase = ProcessoStatus.FormalisticaRealizada;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.SetaRevisao;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.SetaRevisao;
            processo.AlterarStatusDosDocumentos(DocumentoStatus.FormalisticaRealizada);
            processo.Lote.DataFimFormalistica = DateTime.Now;
        }
    }
}