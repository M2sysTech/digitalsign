namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseProcessoAguardandoDigitacao : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoAguardandoDigitacao()
        {
            this.FaseEstaAtiva = x => x.DigitacaoAtivo;
            this.StatusDaFase = ProcessoStatus.AguardandoDigitacao;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Digitado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            foreach (var documento in processo.Documentos)
            {
                documento.Status = documento.PossuiPendenciaDeDigitacao() ?
                    DocumentoStatus.ParaDigitacao :
                    DocumentoStatus.StatusDigitado;
            }

            processo.Status = processo.TemDocumentoComStatus(DocumentoStatus.ParaDigitacao) ?
                ProcessoStatus.AguardandoDigitacao :
                ProcessoStatus.Digitado;
        }
    }
}