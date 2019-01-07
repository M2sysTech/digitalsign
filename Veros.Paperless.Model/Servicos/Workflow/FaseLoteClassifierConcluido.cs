namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseLoteClassifierConcluido : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteClassifierConcluido()
        {
            this.StatusDaFase = LoteStatus.ClassifierExecutado;
            this.StatusSeFaseEstiverInativa = LoteStatus.SetaIdentificacao;
            this.FaseEstaAtiva = x => x.ClassifierAtivo;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.Status = LoteStatus.SetaIdentificacao;
        }
    }
}