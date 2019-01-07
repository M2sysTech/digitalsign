namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using System.Linq;
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class ReordenaPaginasTriagemServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;

        public ReordenaPaginasTriagemServico(
            IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            IPaginaRepositorio paginaRepositorio, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.paginaRepositorio = paginaRepositorio;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
        }

        public void Executar(AcaoDeTriagemPreOcr acao, LoteTriagemViewModel lote)
        {
            var ordemPagina = 1;
            var ordemDocumento = 1;
            var documentoAtual = new DocumentoParaSeparacaoViewModel();

            foreach (var paginaId in acao.Paginas)
            {
                var documentoDaPagina = lote.ObterDocumentoDaPagina(paginaId);
                var pagina = lote.ObterPagina(paginaId);

                if (documentoAtual != documentoDaPagina)
                {
                    ordemDocumento++;
                    documentoAtual = documentoDaPagina;

                    if (ordemDocumento != documentoAtual.Ordem)
                    {
                        documentoAtual.NovaOrdem = ordemDocumento;
                        this.documentoRepositorio.AlterarOrdem(documentoAtual.Id, documentoAtual.NovaOrdem);
                        this.documentoRepositorio.AlterarOrdemPais(documentoAtual.Id, documentoAtual.NovaOrdem);

                        this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoMudancaDeOrdem,
                        documentoAtual.Id,
                        string.Format("Mudança de ordem. De #{0} para #{1}. [{2}]", documentoAtual.Ordem, documentoAtual.NovaOrdem, lote.Fase));

                        lote.MarcaDocumentoManipulado(documentoAtual.Id);

                        this.AtualizarOrdemDocumentoNovo(lote, documentoAtual);
                    }
                }

                var ordemOriginal = pagina.Ordem;
                if (ordemOriginal != ordemPagina)
                {
                    pagina.Ordem = ordemPagina;
                    this.paginaRepositorio.AlterarOrdem(pagina.Id, ordemPagina);

                    this.gravaLogDaPaginaServico.Executar(LogPagina.AcaoMudarOrdem, 
                        paginaId, pagina.DocumentoAtualId(), 
                        string.Format("Mudança de ordem. De #{0} para #{1}. [{2}]", ordemOriginal, ordemPagina, lote.Fase));

                    lote.MarcaDocumentoManipulado(pagina.DocumentoAtualId());
                }

                ordemPagina++;
            }
        }

        private void AtualizarOrdemDocumentoNovo(LoteTriagemViewModel loteParaSeparacao, DocumentoParaSeparacaoViewModel documento)
        {
            var documentoNovo = loteParaSeparacao.DocumentosNovos.FirstOrDefault(x => x.Id == documento.Id);

            if (documentoNovo == null)
            {
                return;
            }

            documentoNovo.Ordem = documento.NovaOrdem;
            this.documentoRepositorio.Salvar(documentoNovo);
        }
    }
}
