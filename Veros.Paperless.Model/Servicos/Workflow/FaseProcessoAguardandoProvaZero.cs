namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class FaseProcessoAguardandoProvaZero : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;

        public FaseProcessoAguardandoProvaZero(
            IRegraVioladaRepositorio regraVioladaRepositorio)
        {
            this.regraVioladaRepositorio = regraVioladaRepositorio;

            this.FaseEstaAtiva = x => x.ProvaZeroAtivo;
            this.StatusDaFase = ProcessoStatus.AguardandoProvaZero;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.AguardandoFormalistica;
        }

        protected override void ProcessarFase(Processo processo)
        {
            if (processo.PossuiDocumentoDoTipo(TipoDocumento.CodigoNaoIdentificado))
            {
                processo.Status = ProcessoStatus.AguardandoMontagem;
                processo.Lote.Status = LoteStatus.AguardandoIdentificacao;
                return;
            }

            if (this.regraVioladaRepositorio.ExisteRegraVioladaSemTratamento(
                processo.Id, Regra.FaseProvaZero))
            {
                processo.Status = ProcessoStatus.AguardandoProvaZero;
                return;
            }

            if (this.ExisteDocumentoPendenteDeProvaZero(processo) == false)
            {
                processo.AlterarStatusDosDocumentos(DocumentoStatus.ParaValidacao);
                ////TODO: Voltar para validação
                ////processo.Status = ProcessoStatus.ProvaZeroRealizada;
                processo.Status = ProcessoStatus.AguardandoValidacao;
            }
        }

        private bool ExisteDocumentoPendenteDeProvaZero(Processo processo)
        {
            var documentos = processo.Documentos.FirstOrDefault(documento =>
                documento.Virtual == false &&
                documento.Indexacao.Any(x => string.IsNullOrEmpty(x.ValorFinal) 
                                        && (x.Campo.DuplaDigitacao || x.Campo.Digitavel)));

            return documentos != null;
        }
    }
}