namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseProcessoSetaRevisao : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoSetaRevisao()
        {
            this.FaseEstaAtiva = x => x.FormalisticaAtiva;
            this.StatusDaFase = ProcessoStatus.SetaRevisao;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.RevisaoRealizada;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.AguardandoRevisao;
            processo.AlterarStatusDosDocumentos(DocumentoStatus.AguardandoRevisao);
        }
    }
}