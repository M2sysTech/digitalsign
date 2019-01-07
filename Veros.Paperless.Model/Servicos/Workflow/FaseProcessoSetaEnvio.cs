namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseProcessoSetaEnvio : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoSetaEnvio()
        {
            this.FaseEstaAtiva = x => x.EnvioAtivo;
            this.StatusDaFase = ProcessoStatus.Faturamento;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.EnvioRealizado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.AguardandoEnvio;
            processo.AlterarStatusDosDocumentos(DocumentoStatus.StatusParaEnvio);
        }
    }
}
