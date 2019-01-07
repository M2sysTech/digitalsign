namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseLoteQualidadeCefFinalizado : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteQualidadeCefFinalizado()
        {
            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = LoteStatus.ControleQualidadeCefRealizado;
            this.StatusSeFaseEstiverInativa = LoteStatus.Faturamento;
        }

        protected override void ProcessarFase(Lote lote)
        {
            lote.DataFaturamento = DateTime.Now; 
            lote.Status = LoteStatus.Faturamento;
            
            foreach (var processo in lote.Processos)
            {
                processo.Status = ProcessoStatus.Faturamento;
            }
        }
    }
}
