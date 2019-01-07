namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseProcessoAguardandoConsulta : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoAguardandoConsulta()
        {
            this.FaseEstaAtiva = x => x.ConsultaAtivo;
            this.StatusDaFase = ProcessoStatus.AguardandoConsulta;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Consultado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.Consultado;
        }
    }
}