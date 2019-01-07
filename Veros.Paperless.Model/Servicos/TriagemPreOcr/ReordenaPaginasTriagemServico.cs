namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class ReordenaDocumentosTriagemServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public ReordenaDocumentosTriagemServico(
            IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(LoteTriagemViewModel lote)
        {
            var documentosOrdenados = this.ObterDocumentosOrdenados(lote);

            foreach (var documento in documentosOrdenados)
            {
                this.documentoRepositorio.AlterarOrdem(documento.Id, documento.NovaOrdem);
                this.documentoRepositorio.AlterarOrdemPais(documento.Id, documento.NovaOrdem);

                this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoMudancaDeOrdem,
                    documento.Id,
                    string.Format("Mudança de ordem. De #{0} para #{1}", documento.Ordem, documento.NovaOrdem));

                this.AtualizarOrdemDocumentoNovo(lote, documento);
            }
        }

        private IEnumerable<DocumentoParaSeparacaoViewModel> ObterDocumentosOrdenados(LoteTriagemViewModel lote)
        {
            var paginas = lote.ObterPaginas();
            var ordemDocumento = 1;
            var documentosOrdenados = new List<DocumentoParaSeparacaoViewModel>();

            var listaDeDocumentacaoGeral = this.documentoRepositorio
                .ObterDocumentosDoLotePorTipo(lote.LoteId, TipoDocumento.CodigoDocumentoGeral);

            foreach (var pagina in paginas.OrderBy(x => x.Ordem))
            {
                var documentoId = pagina.DocumentoAtualId();

                if (documentosOrdenados.Any(x => x.Id == documentoId) ||
                    listaDeDocumentacaoGeral.Any(x => x.Id == documentoId))
                {
                    continue;
                }

                var documento = this.ObterDocumento(documentoId, lote);
                documento.NovaOrdem = ordemDocumento;

                documentosOrdenados.Add(documento);
                ordemDocumento++;
            }

            return documentosOrdenados;
        }

        private DocumentoParaSeparacaoViewModel ObterDocumento(int documentoId, LoteTriagemViewModel loteParaSeparacao)
        {
            var documento = loteParaSeparacao.Documentos.FirstOrDefault(x => x.Id == documentoId);
            return documento ?? new DocumentoParaSeparacaoViewModel { Id = documentoId };
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
