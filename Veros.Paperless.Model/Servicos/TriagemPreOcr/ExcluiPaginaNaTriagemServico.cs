namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class ExcluiPaginaNaTriagemServico : IExcluiPaginaNaTriagemServico
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public ExcluiPaginaNaTriagemServico(
            IPaginaRepositorio paginaRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote)
        {
            var paginaViewModel = lote.ObterPagina(acao.PrimeiraPagina);
            paginaViewModel.Status = PaginaStatus.StatusExcluida;
            var documentoId = paginaViewModel.DocumentoAtualId();

            this.paginaRepositorio.AlterarStatus(paginaViewModel.Id, PaginaStatus.StatusExcluida);

            this.documentoRepositorio.AlterarMarca(documentoId, Documento.MarcaDeAlteradoNaSeparacao);
            lote.MarcaDocumentoManipulado(documentoId);

            switch (acao.Tipo)
            {
                case AcaoDeTriagemPreOcr.ExcluirPagina:
                    this.gravaLogDaPaginaServico.Executar(
                        LogPagina.AcaoPaginaExcluida,
                        paginaViewModel.Id,
                        documentoId,
                        string.Format("Página removida. MDoc Anterior #{0} [{1}]", documentoId, lote.Fase));
                    break;
                default:
                    this.gravaLogDaPaginaServico.Executar(
                        LogPagina.AcaoFolhaExcluida,
                        paginaViewModel.Id,
                        documentoId,
                        string.Format("Folha separadora removida. MDoc Anterior #{0} [{1}]", documentoId, lote.Fase));
                    break;
            }           

            ////this.gravaLogDoDocumentoServico.Executar(
            ////    LogDocumento.AcaoPaginaRemovida,
            ////    documentoId,
            ////    string.Format("Página #{0} foi removida [{1}]", paginaViewModel.Id, lote.Fase));
        }
    }
}
