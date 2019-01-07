namespace Veros.Paperless.Model.Servicos.Separacao
{
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class RessuscitaPaginaNaSeparacaoServico : IRessuscitaPaginaNaSeparacaoServico
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public RessuscitaPaginaNaSeparacaoServico(
            IPaginaRepositorio paginaRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, IDocumentoRepositorio documentoRepositorio)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Executar(AcaoDeSeparacao acao, LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var novoDocumentoId = this.ObterDocumentoDaPagina(acao.PrimeiraPagina, loteParaSeparacao);
            
            var pagina = this.paginaRepositorio.ObterPorId(acao.PrimeiraPagina);
            var documentoAnterior = this.documentoRepositorio.ObterPorId(pagina.Documento.Id);

            var novoDocumento = documentoAnterior.Id == novoDocumentoId ?
                documentoAnterior : 
                this.documentoRepositorio.ObterPorId(novoDocumentoId);

            if (novoDocumento.TipoDocumento.Id == TipoDocumento.CodigoDocumentoGeral)
            {
                novoDocumentoId = loteParaSeparacao.ObterDocumentoDaPagina(pagina.Id).Id;
                novoDocumento = this.documentoRepositorio.ObterPorId(novoDocumentoId);
            }
            
            pagina.Status = PaginaStatus.StatusParaReconhecimento;
            pagina.Documento = novoDocumento;
            this.paginaRepositorio.Salvar(pagina);
            
            this.MarcaDocumentoAlterado(novoDocumento, loteParaSeparacao);
            
            if (documentoAnterior.Id != novoDocumento.Id)
            {
                this.MarcaDocumentoAlterado(documentoAnterior, loteParaSeparacao);
            }

            this.gravaLogDoDocumentoServico.Executar(
                LogDocumento.AcaoPaginaRessucitada,
                pagina.Documento.Id, 
                string.Format("Página {0} foi ressucitada na separação manual. Doc anterior #{1}", pagina.Id, documentoAnterior.Id));
        }

        private int ObterDocumentoDaPagina(int paginaId, LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var paginaParaSeparacao = loteParaSeparacao.ObterPagina(paginaId);
            return paginaParaSeparacao.DocumentoAtualId();
        }

        private void MarcaDocumentoAlterado(Documento documento, LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            this.documentoRepositorio.AlterarMarca(documento.Id, Documento.MarcaDeAlteradoNaSeparacao);

            if (documento.TipoDocumento.Id != TipoDocumento.CodigoDocumentoGeral)
            {
                this.documentoRepositorio.AlterarStatus(documento.Id, DocumentoStatus.StatusParaReconhecimento);
            }

            loteParaSeparacao.MarcaDocumentoManipulado(documento.Id);   
        }
    }
}
