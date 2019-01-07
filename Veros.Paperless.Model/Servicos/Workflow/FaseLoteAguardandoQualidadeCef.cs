namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseLoteAguardandoQualidadeCef : FaseDeWorkflow<Lote, LoteStatus>
    {
        private readonly ILogLoteServico logLoteServico;
        
        public FaseLoteAguardandoQualidadeCef(ILogLoteServico logLoteServico)
        {
            this.logLoteServico = logLoteServico;
            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = LoteStatus.AguardandoControleQualidadeCef;
            this.StatusSeFaseEstiverInativa = LoteStatus.Faturamento;
        }

        protected override void ProcessarFase(Lote lote)
        {
            if (lote.PacoteProcessado.StatusPacote != StatusPacote.AprovadoNaQualidade)
            {
                return;
            }

            lote.DataFaturamento = DateTime.Now;
            lote.Status = LoteStatus.ControleQualidadeCefRealizado;
        }
    }
}
