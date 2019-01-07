namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseLoteSetaTriagemPreOcr : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteSetaTriagemPreOcr()
        {
            this.StatusDaFase = LoteStatus.SetaTriagem;
            this.StatusSeFaseEstiverInativa = LoteStatus.SetaReconhecimento;
            this.FaseEstaAtiva = x => x.TriagemPreOcr;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.Status = LoteStatus.AguardandoTriagem;

            foreach (var processo in lote.Processos)
            {
                processo.Status = ProcessoStatus.AguardandoTriagem;
            }
        }
    }
}