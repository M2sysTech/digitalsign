namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseProcessoSetaConsulta : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoSetaConsulta()
        {
            this.FaseEstaAtiva = x => x.ConsultaAtivo;
            this.StatusDaFase = ProcessoStatus.SetaConsulta;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Consultado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.Consultado;
        }
    }
}