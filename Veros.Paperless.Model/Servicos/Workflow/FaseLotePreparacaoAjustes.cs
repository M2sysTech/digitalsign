namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using Entidades;

    public class FaseLotePreparacaoAjustes : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLotePreparacaoAjustes()
        {
            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = LoteStatus.AguardandoPreparacaoAjustes;
            this.StatusSeFaseEstiverInativa = LoteStatus.PreparacaoAjustesConcluidos;
        }

        protected override void ProcessarFase(Lote lote)
        {
            foreach (var processo in lote.Processos)
            {
                foreach (var documento in processo.Documentos.Where(x => x.Status == DocumentoStatus.AjustePreparacao))
                {
                    documento.Status = DocumentoStatus.AjustePreparacaoRealizada;
                }

                lote.Status = LoteStatus.AguardandoAjustes;
                processo.Status = ProcessoStatus.AguardandoAjuste;
                processo.HoraInicio = null;
            }
        }
    }
}
