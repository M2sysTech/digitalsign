namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using Entidades;
    using Importacao;
    using Repositorios;
    using ViewModel;

    public class CriaDocumentoNaTriagemServico : ICriaDocumentoNaTriagemServico
    {
        private readonly IDocumentoFabrica documentoFabrica;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;

        public CriaDocumentoNaTriagemServico(
            IDocumentoFabrica documentoFabrica, 
            IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            IPaginaRepositorio paginaRepositorio, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico)
        {
            this.documentoFabrica = documentoFabrica;
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.paginaRepositorio = paginaRepositorio;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
        }

        public void Executar(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote)
        {
            var documento = this.CriarDocumento(acao, lote);
            lote.Documentos.Add(DocumentoParaSeparacaoViewModel.Criar(documento));
            lote.DocumentosNovos.Add(documento);
            lote.MarcaDocumentoManipulado(documento.Id);

            this.SalvarPaginas(documento, acao, lote);
        }

        private void SalvarPaginas(Documento documento, AcaoDeTriagemPreOcr acao, LoteTriagemViewModel loteViewModel)
        {
            foreach (var paginaId in acao.Paginas)
            {
                var pagina = this.paginaRepositorio.ObterPorId(paginaId);
                var documentoOriginal = this.documentoRepositorio.ObterPorId(pagina.Documento.Id);

                var paginaViewModel = loteViewModel.ObterPagina(paginaId);
                
                var paginaEstavaExcluida = paginaViewModel == null ||
                    paginaViewModel.Status == PaginaStatus.StatusExcluida;

                if (documentoOriginal.TipoDocumento.Id != TipoDocumento.CodigoDocumentoGeral)
                {
                    loteViewModel.MarcaDocumentoManipulado(documentoOriginal.Id);
                }

                if ((documentoOriginal.TipoDocumento.Id == TipoDocumento.CodigoDocumentoGeral &&
                    documentoOriginal.Status == DocumentoStatus.Excluido) ||
                    paginaEstavaExcluida)
                {
                    pagina.Status = PaginaStatus.StatusExcluida;
                }

                pagina.Documento = documento;
                this.paginaRepositorio.Salvar(pagina);

                documento.Paginas.Add(pagina);

                this.documentoRepositorio.AlterarMarca(documentoOriginal.Id, Documento.MarcaDeAlteradoNaSeparacao);
                loteViewModel.MarcaDocumentoManipulado(documentoOriginal.Id);

                this.gravaLogDaPaginaServico.Executar(LogPagina.AcaoDocumentoCriadoNaSeparacao,
                    pagina.Id,
                    pagina.Documento.Id,
                    string.Format("Página movida para documento #{0}, documento anterior era #{1}. [{2}]", documento.Id, documentoOriginal.Id, loteViewModel.Fase));

                loteViewModel.ObterPagina(paginaId).NovoDocumentoId = documento.Id;
            }
        }

        private Documento CriarDocumento(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote)
        {
            var processo = new Processo
            {
                Id = lote.ProcessoId,
                Lote = new Lote { Id = lote.LoteId }
            };

            var tipoDocumento = new TipoDocumento { Id = acao.TipoDocumentoId };

            var documento = this.documentoFabrica.Criar(processo, tipoDocumento, "1");
            documento.Status = DocumentoStatus.TransmissaoOk;
            documento.Ordem = acao.NovoDocumentoOrdem;
            documento.Marca = Documento.MarcaDeCriadoNaSeparacao;
            documento.Versao = "0";
            this.documentoRepositorio.Salvar(documento);

            this.gravaLogDoDocumentoServico.Executar(
                LogDocumento.AcaoDocumentoNaTriagem,
                documento.Id,
                string.Format("Documento criado [{0}]. Tipo: {1}", lote.Fase, tipoDocumento.Id));

            return documento;
        }
    }
}