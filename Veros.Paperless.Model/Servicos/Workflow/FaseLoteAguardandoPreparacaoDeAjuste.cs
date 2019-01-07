namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using Entidades;

    public class FaseLoteAguardandoPreparacaoDeAjuste : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteAguardandoPreparacaoDeAjuste()
        {
            this.FaseEstaAtiva = x => x.IdentificacaoAtivo;
            this.StatusDaFase = LoteStatus.AguardandoAjustes;
            this.StatusSeFaseEstiverInativa = LoteStatus.AguardandoAjustes;
        }

        protected override void ProcessarFase(Lote lote)
        {
            var processo = lote.Processos.FirstOrDefault();

            if (processo.Documentos.Any(x => x.Status == DocumentoStatus.TelaAjuste))
            {
                lote.Status = LoteStatus.AguardandoAjustes;
                processo.Status = ProcessoStatus.AguardandoAjuste;
            }
        }
    }
}