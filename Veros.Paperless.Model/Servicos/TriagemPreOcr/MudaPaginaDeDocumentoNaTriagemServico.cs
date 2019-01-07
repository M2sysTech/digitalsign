namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class MudaPaginaDeDocumentoNaTriagemServico
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;

        public MudaPaginaDeDocumentoNaTriagemServico(
            IPaginaRepositorio paginaRepositorio, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
        }

        public void Executar(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote)
        {
            if (acao.NovoDocumentoId < 1)
            {
                return;
            }

            var paginaView = lote.ObterPagina(acao.PrimeiraPagina);
            paginaView.NovoDocumentoId = acao.NovoDocumentoId;

            this.paginaRepositorio.AlterarDocumento(paginaView.Id, acao.NovoDocumentoId);

            this.gravaLogDaPaginaServico.Executar(LogPagina.AcaoDocumentoCriadoNaSeparacao,
                    paginaView.Id,
                    paginaView.NovoDocumentoId,
                    string.Format("Página movida para documento #{0}, documento anterior era #{1}. {2}", acao.NovoDocumentoId, acao.DocumentoOrigemId, lote.Fase));

            lote.MarcaDocumentoManipulado(acao.NovoDocumentoId);
            lote.MarcaDocumentoManipulado(acao.DocumentoOrigemId);
        }
    }
}
