namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseProcessoConsultado : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoConsultado()
        {
            this.FaseEstaAtiva = x => x.ConsultaAtivo;
            this.StatusDaFase = ProcessoStatus.Consultado;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.SetaValidacao;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.SetaValidacao;
            processo.Lote.DataFimConsulta = DateTime.Now;
            processo.AlterarStatusDosDocumentos(DocumentoStatus.StatusDeConsultaRealizado);
        }
    }
}