namespace Veros.Paperless.Model.Servicos.Separacao
{
    using Repositorios;
    using ViewModel;

    public class ObtemLoteParaSeparacaoServico : IObtemLoteParaSeparacaoServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly MontaDocumentosParaSeparacaoServico montaDocumentosParaSeparacaoServico;

        public ObtemLoteParaSeparacaoServico(IProcessoRepositorio processoRepositorio, 
            MontaDocumentosParaSeparacaoServico montaDocumentosParaSeparacaoServico)
        {
            this.processoRepositorio = processoRepositorio;
            this.montaDocumentosParaSeparacaoServico = montaDocumentosParaSeparacaoServico;
        }

        public LoteParaSeparacaoViewModel Executar(int processoId)
        {
            var processo = this.processoRepositorio.ObterComPacote(processoId);

            var lote = LoteParaSeparacaoViewModel.Criar(processo);

            lote.Documentos = this.montaDocumentosParaSeparacaoServico.Executar(processo.Lote);

            return lote;
        }
    }
}
