namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseProcessoValidado : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        public FaseProcessoValidado()
        {
            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = ProcessoStatus.Validado;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.SetaProvaZero;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.SetaProvaZero;
            processo.AlterarStatusDosDocumentos(DocumentoStatus.StatusValidado);

            if (processo.Lote.DataFimValidacao.HasValue == false || processo.Lote.DataFimValidacao.GetValueOrDefault().Year < 2000)
            {
                processo.Lote.DataFimValidacao = DateTime.Now;
            }
        }
    }
}