namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseLoteCapturaFinalizada : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteCapturaFinalizada()
        {
            this.StatusDaFase = LoteStatus.CapturaFinalizada;
            this.StatusSeFaseEstiverInativa = LoteStatus.ParaTransmitir;
            this.FaseEstaAtiva = x => true;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.Status = LoteStatus.EmRecepcao;
        }
    }
}