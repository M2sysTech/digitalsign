namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseLoteTransmissaoFinalizada : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteTransmissaoFinalizada()
        {
            this.StatusDaFase = LoteStatus.AguardandoTransmissao;
            this.StatusSeFaseEstiverInativa = LoteStatus.AguardandoSeparacaoClassifier;
            this.FaseEstaAtiva = x => x.ReconhecimentoAtivo;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.Status = LoteStatus.AguardandoSeparacaoClassifier;
        }
    }
}