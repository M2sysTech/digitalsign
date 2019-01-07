namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;

    public class FaseLoteAguardandoCaptura : FaseDeWorkflow<Lote, LoteStatus>
    {
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;

        public FaseLoteAguardandoCaptura(IGravaLogDoLoteServico gravaLogDoLoteServico)
        {
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.StatusDaFase = LoteStatus.EmCaptura;
            this.StatusSeFaseEstiverInativa = LoteStatus.ParaTransmitir;
            this.FaseEstaAtiva = x => true;
        }

        protected override void ProcessarFase(Lote lote)
        {
            var tempo = DateTime.Now.Subtract(lote.DataCriacao).TotalDays;

            if (tempo < 90)
            {
                return;
            }

            foreach (var processo in lote.Processos)
            {
                processo.AlterarStatusDosDocumentos(DocumentoStatus.Excluido);
                processo.Status = ProcessoStatus.Excluido;
            }

            lote.Status = LoteStatus.Excluido;
            lote.DataFinalizacao = DateTime.Now;
            this.gravaLogDoLoteServico.Executar(LogLote.AcaoLoteExcluidoNaCaptura, lote.Id, "Lote foi excluído por tempo parado na captura");
        }
    }
}