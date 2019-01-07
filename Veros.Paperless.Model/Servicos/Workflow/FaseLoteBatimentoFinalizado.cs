namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseLoteBatimentoFinalizado : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteBatimentoFinalizado()
        {
            this.FaseEstaAtiva = x => x.MontagemAtivo;
            this.StatusDaFase = LoteStatus.Batido;
            this.StatusSeFaseEstiverInativa = LoteStatus.Montado;
        }

        protected override void ProcessarFase(Lote lote)
        {
            foreach (var processo in lote.Processos)
            {
                processo.Status = ProcessoStatus.AguardandoMontagem;
            }

            lote.Status = LoteStatus.Montado;
        }
    }
}