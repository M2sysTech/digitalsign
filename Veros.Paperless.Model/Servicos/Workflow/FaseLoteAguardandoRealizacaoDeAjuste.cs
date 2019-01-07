namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using Entidades;

    public class FaseLoteAguardandoRealizacaoDeAjuste : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteAguardandoRealizacaoDeAjuste()
        {
            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = LoteStatus.AguardandoRealizacaoAjustes;
            this.StatusSeFaseEstiverInativa = LoteStatus.AjustesConcluidos;
        }

        protected override void ProcessarFase(Lote lote)
        {
            var processo = lote.Processos.First();

            if (this.ExisteDocumentoAguardandoGerarPdf(processo))
            {
                return;
            }
            
            processo.HoraInicio = null;
            processo.Status = ProcessoStatus.AjusteConcluido;
            processo.Lote.Status = LoteStatus.AjustesConcluidos;   
        }

        private bool ExisteDocumentoAguardandoGerarPdf(Processo processo)
        {
            return processo.Documentos.Any(x => 
                x.Status == DocumentoStatus.StatusParaReconhecimentoPosAjuste ||
                x.Status == DocumentoStatus.StatusParaAguardandoReconhecimentoPosAjuste);
        }
    }
}
