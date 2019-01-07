namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseProcessoSetaErro : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoSetaErro()
        {
            this.FaseEstaAtiva = x => true;
            this.StatusDaFase = ProcessoStatus.SetaErro;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Erro;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.Erro;
        }
    }
}