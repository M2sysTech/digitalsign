namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using EngineDeRegras;
    using Entidades;

    public class FaseProcessoSetaProvaZero : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly IExecutorDeRegra executorDeRegra;

        public FaseProcessoSetaProvaZero(IExecutorDeRegra executorDeRegra)
        {
            this.executorDeRegra = executorDeRegra;

            this.FaseEstaAtiva = x => x.ProvaZeroAtivo;
            this.StatusDaFase = ProcessoStatus.SetaProvaZero;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.AguardandoFormalistica;
        }

        protected override void ProcessarFase(Processo processo)
        {
            if (this.executorDeRegra.ExistemRegrasAtendidas(Regra.FaseProvaZero, processo))
            {
                processo.Status = ProcessoStatus.AguardandoProvaZero;
                return;
            }

            foreach (var documento in processo.Documentos.Where(x => x.Status != DocumentoStatus.Excluido))
            {
                if (documento.Indexacao.Any(x => x.Campo.DuplaDigitacao && string.IsNullOrEmpty(x.ValorFinal)))
                {
                    documento.Status = DocumentoStatus.StatusEmProvaZero;
                }
            }

            processo.Status = processo.TemDocumentoComStatus(DocumentoStatus.StatusEmProvaZero) ?
                ProcessoStatus.AguardandoProvaZero :
                ProcessoStatus.ProvaZeroRealizada;
        }
    }
}