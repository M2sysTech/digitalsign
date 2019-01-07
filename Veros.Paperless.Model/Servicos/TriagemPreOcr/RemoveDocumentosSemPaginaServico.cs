namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using System.Linq;
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class RemoveDocumentosSemPaginaServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public RemoveDocumentosSemPaginaServico(IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(LoteTriagemViewModel lote)
        {
            var paginas = lote.ObterPaginas();

            foreach (var documento in lote.Documentos)
            {
                if (paginas.Any(x => x.DocumentoAtualId() == documento.Id && x.Status != PaginaStatus.StatusExcluida))
                {
                    continue;
                }

                this.Excluir(documento.Id, lote);

                var documentosFilhos = this.documentoRepositorio.ObterFilhos(lote.LoteId, documento.Id);

                foreach (var documentoFilho in documentosFilhos)
                {
                    this.Excluir(documentoFilho.Id, lote);
                }
            }
        }

        private void Excluir(int documentoId, LoteTriagemViewModel lote)
        {
            this.documentoRepositorio.AlterarStatus(documentoId, DocumentoStatus.Excluido);
            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoExcluidoNaClassificacao, documentoId, "Documento excluído porque ficou sem páginas. " + lote.Fase);

            if (lote.DocumentosNovos == null)
            {
                return;
            }

            var documentoNovo = lote.DocumentosNovos.FirstOrDefault(x => x.Id == documentoId);

            if (documentoNovo != null)
            {
                documentoNovo.Status = DocumentoStatus.Excluido;
            }
        }
    }
}
