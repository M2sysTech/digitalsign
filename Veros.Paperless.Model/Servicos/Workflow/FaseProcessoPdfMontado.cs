namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseProcessoPdfMontado : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoPdfMontado()
        {
            this.FaseEstaAtiva = x => x.ClassifierAtivo;
            this.StatusDaFase = ProcessoStatus.PdfMontado;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.AguardandoExportacao;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.SetaExportacao;
        }
    }
}