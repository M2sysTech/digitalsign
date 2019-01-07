namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseProcessoAguardandoValidacao : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoAguardandoValidacao()
        {
            this.FaseEstaAtiva = x => x.MontagemAtivo;
            this.StatusDaFase = ProcessoStatus.AguardandoValidacao;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Validado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            if (processo.TemDocumentoComStatus(DocumentoStatus.ParaValidacao) == false)
            {
                processo.Status = ProcessoStatus.Validado;
            }
        }
    }
}