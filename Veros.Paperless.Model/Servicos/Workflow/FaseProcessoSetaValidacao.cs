namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using EngineDeRegras;
    using Entidades;

    public class FaseProcessoSetaValidacao : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly IExecutorDeRegra executorDeRegra;

        public FaseProcessoSetaValidacao(IExecutorDeRegra executorDeRegra)
        {
            this.executorDeRegra = executorDeRegra;

            this.FaseEstaAtiva = x => x.ValidacaoAtivo;
            this.StatusDaFase = ProcessoStatus.SetaValidacao;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Validado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            processo.Status = ProcessoStatus.AguardandoValidacao;
            
            this.executorDeRegra.ExistemRegrasAtendidas(Regra.FaseValidacao, processo);

            foreach (var documento in processo.Documentos)
            {
                this.GravarValoresFinais(documento);

                documento.Status = this.ExisteDivergenciaEntreValores(documento) ?
                    DocumentoStatus.ParaValidacao :
                    DocumentoStatus.StatusValidado;
            }
        }

        private bool ExisteDivergenciaEntreValores(Documento documento)
        {
            return documento.ExisteDivergenciaEmAlgumaIndexacao();
        }

        private void GravarValoresFinais(Documento documento)
        {
            //// TODO: simplificar, talvez encapsular em alguma entidade
            foreach (var indexacao in documento.Indexacao.Where(x => x.Campo.Digitavel && string.IsNullOrEmpty(x.ValorFinal)))
            {
                if (string.IsNullOrEmpty(indexacao.PrimeiroValor) == false && 
                    string.IsNullOrEmpty(indexacao.SegundoValor) == false &&
                    indexacao.PrimeiroValor.Trim().ToUpper() == indexacao.SegundoValor.Trim().ToUpper() &&
                    indexacao.PrimeiroValor != "?" && 
                    indexacao.PrimeiroValor != "#" && 
                    string.IsNullOrWhiteSpace(indexacao.PrimeiroValor) == false
                    )
                {
                    indexacao.ValorFinal = indexacao.PrimeiroValor.Trim();
                }
            }
        }
    }
}