namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseLoteAssinaturaFinalizada : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteAssinaturaFinalizada()
        {
            this.FaseEstaAtiva = x => x.AssinaturaDigitalAtivo;
            this.StatusDaFase = LoteStatus.AssinaturaFinalizada;
            this.StatusSeFaseEstiverInativa = LoteStatus.EmExportacaoParaCloud;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.Status = LoteStatus.EmExportacaoParaCloud;            
        }
    }
}