namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseProcessoDigitado : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoDigitado()
        {
            this.FaseEstaAtiva = x => x.DigitacaoAtivo;
            this.StatusDaFase = ProcessoStatus.Digitado;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.SetaConsulta;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.SetaConsulta;
            processo.HoraInicio = null;
            processo.AlterarStatusDosDocumentos(DocumentoStatus.StatusDigitado);
            processo.Lote.DataFimDigitacao = DateTime.Now;
        }
    }
}