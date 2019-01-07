namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;
    using Framework;

    public class FaseLoteAguardandoFaturamento : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteAguardandoFaturamento()
        {
            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = LoteStatus.Faturamento;
            this.StatusSeFaseEstiverInativa = LoteStatus.Finalizado;
        }

        protected override void ProcessarFase(Lote lote)
        {
            Log.Application.DebugFormat("Faturamento - falta implementar.");
        }
    }
}
