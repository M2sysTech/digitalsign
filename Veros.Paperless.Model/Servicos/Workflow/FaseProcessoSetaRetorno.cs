namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseProcessoSetaRetorno : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoSetaRetorno()
        {
            this.FaseEstaAtiva = x => x.RetornoAtivo;
            this.StatusDaFase = ProcessoStatus.SetaRetorno;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Finalizado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.AguardandoRetorno;
            processo.AlterarStatusDosDocumentos(DocumentoStatus.SetaRetorno);
        }
    }
}
