namespace Veros.Paperless.Model.Servicos.Separacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class MontaDocumentosParaSeparacaoServico : IMontaDocumentosParaSeparacaoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;

        public MontaDocumentosParaSeparacaoServico(
            IDocumentoRepositorio documentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
        }

        public IList<DocumentoParaSeparacaoViewModel> Executar(Lote lote)
        {
            var documentos = this.documentoRepositorio.ObterComPaginas(lote);

            ////var documentosOriginais = documentos.Where(x =>
            ////    x.DocumentoPaiId == 0
            ////    && x.Versao == "0"
            ////    && (x.TipoDocumento.Id == TipoDocumento.CodigoDocumentoGeral
            ////        || x.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado
            ////        || x.Marca == Documento.MarcaDeCriadoNaSeparacao
            ////        || x.Marca == Documento.MarcaDeAlteradoNaSeparacao)).ToList();

            var documentosOriginais = documentos.Where(x =>
                x.DocumentoPaiId == 0
                && x.Virtual == false).ToList();

            var documentosClassificados = documentos.Where(x =>
                x.DocumentoPaiId > 0
                && x.Virtual == true).ToList();
            
            var documentosParaSeparacao = new List<DocumentoParaSeparacaoViewModel>();

            var paginas = documentosOriginais.SelectMany(documento => documento.Paginas)
                .Where(x => x.TipoArquivo != "PDF" && x.TipoArquivo != "PNG")
                .OrderBy(x => x.Ordem)
                .ToList();

            DocumentoParaSeparacaoViewModel ultimoDocumentoCriado = null;

            var ordem = 1;
            foreach (var pagina in paginas)
            {
                var documentoParaSeparacao = this.ObterDocumentoParaSeparacao(pagina, documentosParaSeparacao, ultimoDocumentoCriado);
                documentoParaSeparacao.AdicionaPagina(pagina, ordem);

                ultimoDocumentoCriado = documentoParaSeparacao;
                ordem++;
            }

            this.CarregarTipos(documentosParaSeparacao, documentosClassificados);
            
            return documentosParaSeparacao;
        }
        
        private DocumentoParaSeparacaoViewModel ObterDocumentoParaSeparacao(Pagina pagina, 
            IList<DocumentoParaSeparacaoViewModel> documentosParaSeparacao,
            DocumentoParaSeparacaoViewModel ultimoDocumentoCriado)
        {
            if (ultimoDocumentoCriado != null && pagina.Documento.TipoDocumento.Id == TipoDocumento.CodigoDocumentoGeral)
            {
                return ultimoDocumentoCriado;
            }

            var documentoParaSeparacao = documentosParaSeparacao.FirstOrDefault(x => x.Id == pagina.Documento.Id);

            if (documentoParaSeparacao != null)
            {
                return documentoParaSeparacao;
            }

            documentoParaSeparacao = DocumentoParaSeparacaoViewModel.Criar(pagina.Documento);
            documentosParaSeparacao.Add(documentoParaSeparacao);

            return documentoParaSeparacao;
        }
        
        private void CarregarTipos(IEnumerable<DocumentoParaSeparacaoViewModel> documentosParaSeparacao, List<Documento> documentosClassificados)
        {
            foreach (var documentoParaSeparacao in documentosParaSeparacao)
            {
                var documentoClassificado = this.ObtemDocumentoClassificado(documentoParaSeparacao.Id, documentosClassificados);

                if (documentoClassificado == null)
                {
                    continue;
                }

                documentoParaSeparacao.TipoId = documentoClassificado.TipoDocumento.Id;
                documentoParaSeparacao.TipoDescricao = documentoClassificado.TipoDocumento.Description;
            }
        }

        private Documento ObtemDocumentoClassificado(int documentoId, IEnumerable<Documento> documentosClassificados)
        {
            var filhos = documentosClassificados.Where(x => x.DocumentoPaiId == documentoId);

            if (filhos.Any(x => x.Status != DocumentoStatus.Excluido))
            {
                return filhos.FirstOrDefault(x => x.Status != DocumentoStatus.Excluido);
            }

            return filhos.FirstOrDefault();
        }
    }
}
