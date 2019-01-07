namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using Framework.Servicos;
    using Repositorios;
    using ViewModel;

    public class ObtemLoteParaTriagemPreOcrServico : IObtemLoteParaTriagemPreOcrServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IMontaDocumentosParaTriagemServico montaDocumentosParaTriagemServico;

        public ObtemLoteParaTriagemPreOcrServico(
            ISessaoDoUsuario userSession,
            IProcessoRepositorio processoRepositorio, 
            IMontaDocumentosParaTriagemServico montaDocumentosParaTriagemServico)
        {
            this.userSession = userSession;
            this.processoRepositorio = processoRepositorio;
            this.montaDocumentosParaTriagemServico = montaDocumentosParaTriagemServico;
        }

        public LoteTriagemViewModel Obter(int processoId, bool ignorarExcluidas, string fase)
        {
            var processo = this.processoRepositorio.ObterComPacote(processoId);
            this.processoRepositorio.AlterarResponsavel(processoId, this.userSession.UsuarioAtual.Id);

            var lote = LoteTriagemViewModel.Criar(processo, fase);

            lote.Documentos = this.montaDocumentosParaTriagemServico.Executar(processo.Lote, ignorarExcluidas);

            return lote;
        }
    }
}
