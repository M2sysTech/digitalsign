namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseProcessoProvaZeroRealizada : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoProvaZeroRealizada()
        {
            this.FaseEstaAtiva = x => x.ProvaZeroAtivo;
            this.StatusDaFase = ProcessoStatus.ProvaZeroRealizada;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.SetaFormalistica;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.SetaFormalistica;
            processo.HoraInicio = null;
            processo.AlterarStatusDosDocumentos(DocumentoStatus.StatusProvaZeroRealizada);
            processo.Lote.DataFimProvaZero = DateTime.Now;
        }
    }
}