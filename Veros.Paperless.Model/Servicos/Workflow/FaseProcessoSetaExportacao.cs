namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class FaseProcessoSetaExportacao : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly ICampoRepositorio campoRepositorio;
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;

        public FaseProcessoSetaExportacao(
            ICampoRepositorio campoRepositorio, 
            IPacoteRepositorio pacoteRepositorio, 
            IProcessoRepositorio processoRepositorio)
        {
            this.campoRepositorio = campoRepositorio;
            this.pacoteRepositorio = pacoteRepositorio;
            this.processoRepositorio = processoRepositorio;

            this.FaseEstaAtiva = x => x.ExportacaoAtiva;
            this.StatusDaFase = ProcessoStatus.SetaExportacao;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Finalizado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.AlterarStatusDosDocumentos(DocumentoStatus.StatusParaExportacao);

            this.AtualizarTodosOsProcessosDeUmPacoteParaAguardandoExportacao(processo);
        }

        private void AtualizarTodosOsProcessosDeUmPacoteParaAguardandoExportacao(Processo processo)
        {
            var pacote = this.pacoteRepositorio.ObterPacoteDoProcesso(processo);
            var processos = this.processoRepositorio.ObterTodosDoPacote(pacote);

            foreach (var proc in processos)
            {
                if (proc.Status != ProcessoStatus.SetaExportacao && 
                    proc.Status != ProcessoStatus.AguardandoExportacao &&
                    proc.Status != ProcessoStatus.Excluido)
                {
                    return;
                }
            }
            
            this.pacoteRepositorio.MarcarParaExportar(pacote);

            foreach (var processoAguardandoExportacao in processos)
            {
                this.processoRepositorio
                    .AlterarStatus(processoAguardandoExportacao.Id, ProcessoStatus.AguardandoExportacao);
            }
        }
    }
}
