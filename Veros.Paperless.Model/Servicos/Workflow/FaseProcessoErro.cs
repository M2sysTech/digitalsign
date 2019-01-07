namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;
    using Repositorios;

    public class FaseProcessoErro : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly IRemessaRepositorio remessaRepositorio;

        public FaseProcessoErro(IRemessaRepositorio remessaRepositorio)
        {
            this.remessaRepositorio = remessaRepositorio;

            this.FaseEstaAtiva = x => true;
            this.StatusDaFase = ProcessoStatus.Erro;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Erro;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.AlterarStatusDosDocumentos(DocumentoStatus.Erro);
            processo.Status = ProcessoStatus.Erro;

            this.remessaRepositorio.FinalizaRemessaAposExport(processo.Id);

            processo.Lote.Status = LoteStatus.Erro;
        }
    }
}