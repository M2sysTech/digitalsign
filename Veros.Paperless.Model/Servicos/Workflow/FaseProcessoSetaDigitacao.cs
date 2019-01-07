namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public class FaseProcessoSetaDigitacao : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoSetaDigitacao()
        {
            this.FaseEstaAtiva = x => x.DigitacaoAtivo;
            this.StatusDaFase = ProcessoStatus.SetaDigitacao;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Digitado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            if (processo.ExistePeloMenosUmCampoQueRequerDigitacao())
            {
                processo.Status = ProcessoStatus.AguardandoDigitacao;
                return;
            }
             
            processo.Status = ProcessoStatus.Digitado;
        }
    }
}