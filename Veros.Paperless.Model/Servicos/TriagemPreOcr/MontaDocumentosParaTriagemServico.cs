namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class MontaDocumentosParaTriagemServico : IMontaDocumentosParaTriagemServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;

        public MontaDocumentosParaTriagemServico(IDocumentoRepositorio documentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
        }

        public IList<DocumentoParaSeparacaoViewModel> Executar(Lote lote, bool ignorarExcluidas)
        {
            var documentos = this.documentoRepositorio.ObterComPaginas(lote);

            var documentosViewModel = new List<DocumentoParaSeparacaoViewModel>();

            foreach (var documento in documentos.Where(x => 
                x.Virtual == false && 
                x.DocumentoPaiId == 0 &&
                x.Paginas.Any(y => y.Status != PaginaStatus.StatusExcluida || ignorarExcluidas == false))
                .OrderBy(x => x.Ordem))
            {
                var documentoViewModel = this.CriarDocumentoViewModel(documento, ignorarExcluidas);

                if (documentoViewModel.TipoId == TipoDocumento.CodigoNaoIdentificado)
                {
                    var documentoPai = documentos.FirstOrDefault(x => 
                        x.DocumentoPaiId == documentoViewModel.Id && 
                        x.TipoDocumento.Id != TipoDocumento.CodigoNaoIdentificado);

                    if (documentoPai != null)
                    {
                        documentoViewModel.TipoId = documentoPai.TipoDocumento.Id;
                        documentoViewModel.TipoDescricao = documentoPai.TipoDocumento.Description;
                    }
                }

                documentosViewModel.Add(documentoViewModel);
            }

            return documentosViewModel;
        }

        private DocumentoParaSeparacaoViewModel CriarDocumentoViewModel(Documento documento, bool ignorarExcluidas)
        {
            var documentoViewModel = DocumentoParaSeparacaoViewModel.Criar(documento);

            var paginas = documento.Paginas.Where(x => x.Status != PaginaStatus.StatusExcluida || ignorarExcluidas == false).ToList();

            foreach (var pagina in paginas)
            {
                documentoViewModel.AdicionaPagina(pagina, pagina.Ordem);    
            }
            
            return documentoViewModel;
        }
    }
}
