namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;
    using Repositorios;

    public class FaseProcessoFinalizado : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly IRemessaRepositorio remessaRepositorio;

        public FaseProcessoFinalizado(IRemessaRepositorio remessaRepositorio)
        {
            this.remessaRepositorio = remessaRepositorio;
            this.FaseEstaAtiva = x => true;
            this.StatusDaFase = ProcessoStatus.Finalizado;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Finalizado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.AlterarStatusDosDocumentos(DocumentoStatus.StatusFinalizado);

            if (processo.Lote.ProcessosEstaoFinalizados())
            {
                this.remessaRepositorio.FinalizaRemessaAposExport(processo.Id);

                processo.Lote.Status = LoteStatus.Finalizado;
                processo.Lote.DataFinalizacao = DateTime.Now;
            }
        }
    }
}