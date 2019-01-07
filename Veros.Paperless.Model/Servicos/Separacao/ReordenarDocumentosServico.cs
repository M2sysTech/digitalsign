namespace Veros.Paperless.Model.Servicos.Separacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class ReordenarDocumentosServico : IReordenarDocumentosServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public ReordenarDocumentosServico(IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var documentosOrdenados = this.ObterDocumentosOrdenados(loteParaSeparacao);

            foreach (var documento in documentosOrdenados.Where(x => x.Ordem != x.NovaOrdem))
            {
                this.documentoRepositorio.AlterarOrdem(documento.Id, documento.NovaOrdem);
                this.documentoRepositorio.AlterarOrdemPais(documento.Id, documento.NovaOrdem);

                this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoMudancaDeOrdem,
                    documento.Id,
                    string.Format("Mudança de ordem. De #{0} para #{1}", documento.Ordem, documento.NovaOrdem));

                this.AtualizarOrdemDocumentoNovo(loteParaSeparacao, documento);
            }
        }

        private IEnumerable<DocumentoParaSeparacaoViewModel> ObterDocumentosOrdenados(LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var paginas = loteParaSeparacao.ObterPaginas();
            //// começa sempre na 2 (a primeira é capa m2sys)
            var ordemDocumento = 2;
            var listaDeDocumentos = new List<DocumentoParaSeparacaoViewModel>();

            var listaDeDocumentacaoGeral = this.documentoRepositorio
                .ObterDocumentosDoLotePorTipo(loteParaSeparacao.LoteId, TipoDocumento.CodigoDocumentoGeral);

            foreach (var pagina in paginas.OrderBy(x => x.Ordem))
            {
                var documentoId = pagina.DocumentoAtualId();

                if (listaDeDocumentos.Any(x => x.Id == documentoId) ||
                    listaDeDocumentacaoGeral.Any(x => x.Id == documentoId))
                {
                    continue;
                }

                var documento = this.ObterDocumento(documentoId, loteParaSeparacao);
                documento.NovaOrdem = ordemDocumento;

                listaDeDocumentos.Add(documento);
                ordemDocumento++;
            }

            return listaDeDocumentos;
        }

        private DocumentoParaSeparacaoViewModel ObterDocumento(int documentoId, LoteParaSeparacaoViewModel loteParaSeparacao)
        {
            var documento = loteParaSeparacao.Documentos.FirstOrDefault(x => x.Id == documentoId);
            return documento ?? new DocumentoParaSeparacaoViewModel { Id = documentoId };
        }

        private void AtualizarOrdemDocumentoNovo(LoteParaSeparacaoViewModel loteParaSeparacao, DocumentoParaSeparacaoViewModel documento)
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
