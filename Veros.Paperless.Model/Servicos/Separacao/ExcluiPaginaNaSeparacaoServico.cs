namespace Veros.Paperless.Model.Servicos.Separacao
{
    using System.Linq;
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class ExcluiPaginaNaSeparacaoServico : IExcluiPaginaNaSeparacaoServico
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public ExcluiPaginaNaSeparacaoServico(
            IPaginaRepositorio paginaRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico, 
            IDocumentoRepositorio documentoRepositorio)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Executar(AcaoDeSeparacao acao, LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var documentacaoGeral = this.documentoRepositorio
                .ObterPorProcessoETipo(loteParaSeparacao.ProcessoId, TipoDocumento.CodigoDocumentoGeral)
                .FirstOrDefault();

            if (documentacaoGeral == null || documentacaoGeral.Id < 1)
            {
                return;
            }

            var pagina = this.paginaRepositorio.ObterPorId(acao.PrimeiraPagina);
            var documentoOriginal = this.documentoRepositorio.ObterPorId(pagina.Documento.Id);

            pagina.Status = PaginaStatus.StatusExcluida;
            pagina.Documento = new Documento { Id = documentacaoGeral.Id };    
            
            this.paginaRepositorio.Salvar(pagina);

            this.documentoRepositorio.AlterarMarca(documentoOriginal.Id, Documento.MarcaDeAlteradoNaSeparacao);
            ////this.documentoRepositorio.AlterarStatus(documentoOriginal.Id, DocumentoStatus.TransmissaoOk);
            loteParaSeparacao.MarcaDocumentoManipulado(documentoOriginal.Id);

            this.gravaLogDaPaginaServico.Executar(LogPagina.AcaoPaginaExcluida, 
                pagina.Id, 
                pagina.Documento.Id, 
                "Página removida na separação. MDoc Anterior #" + documentoOriginal.Id);

            this.gravaLogDoDocumentoServico.Executar(
                LogDocumento.AcaoPaginaRemovida, 
                documentoOriginal.Id, 
                string.Format("Página #{0} foi removida na separação manual", pagina.Id));
        }
    }
}
