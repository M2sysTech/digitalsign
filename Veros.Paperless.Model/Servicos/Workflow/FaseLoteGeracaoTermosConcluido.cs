namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseLoteGeracaoTermosConcluido : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteGeracaoTermosConcluido()
        {
            this.StatusDaFase = LoteStatus.GeracaoTermosExecutado;
            this.StatusSeFaseEstiverInativa = LoteStatus.GeracaoTermosExecutado;
            this.FaseEstaAtiva = x => x.TermoDeAtuacao;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.Status = LoteStatus.AguardandoAssinatura;
        }
    }
}