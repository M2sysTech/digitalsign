namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;
    using Repositorios;

    public class FaseProcessoAguardandoRevisao : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;

        public FaseProcessoAguardandoRevisao(IRegraVioladaRepositorio regraVioladaRepositorio)
        {
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.FaseEstaAtiva = x => x.FormalisticaAtiva;
            this.StatusDaFase = ProcessoStatus.AguardandoRevisao;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.RevisaoRealizada;
        }

        protected override void ProcessarFase(Processo processo)
        {
            if (this.regraVioladaRepositorio.ExisteRegraPendenteDeRevisao(processo.Id) == false)
            {
                processo.Status = ProcessoStatus.RevisaoRealizada;
            }
        }
    }
}