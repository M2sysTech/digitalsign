namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class UnificaDocumentosServico : IUnificaDocumentosServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public UnificaDocumentosServico(
            IDocumentoRepositorio documentoRepositorio,
            IPaginaRepositorio paginaRepositorio,
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Unificar(Processo processo)
        {
            var tipos = processo.Documentos.Select(x => x.TipoDocumento).Where(x => x.Id != TipoDocumento.CodigoNaoIdentificado).Distinct();

            foreach (var tipoDocumento in tipos)
            {
                var documentos = processo.Documentos.Where(x => x.TipoDocumento.Id == tipoDocumento.Id);

                if (documentos.Count() < 2)
                {
                    continue;
                }

                var documentoFinal = documentos.First();
                var outrosDocumentosDoTipo = documentos.Where(x => x.Id != documentoFinal.Id);

                this.UnificaDocumentos(documentoFinal, outrosDocumentosDoTipo);

                this.SalvarDocumentos(documentos);
            }
        }

        private void SalvarDocumentos(IEnumerable<Documento> documentos)
        {
            foreach (var documento in documentos)
            {
                this.documentoRepositorio.Salvar(documento);

                foreach (var pagina in documento.Paginas)
                {
                    this.paginaRepositorio.Salvar(pagina);
                }
            }
        }

        private void UnificaDocumentos(Documento documentoFinal, IEnumerable<Documento> outrosDocumentosDoTipo)
        {
            foreach (var documento in outrosDocumentosDoTipo)
            {
                this.AdicionarPaginas(documentoFinal, documento.Paginas);
                documento.Status = DocumentoStatus.Excluido;
                documento.Paginas.Clear();
            }
        }

        private void AdicionarPaginas(Documento documentoFinal, IEnumerable<Pagina> paginas)
        {
            foreach (var pagina in paginas)
            {
                pagina.Ordem = documentoFinal.Paginas.Count;
                pagina.Documento = documentoFinal;
                
                this.gravaLogDoDocumentoServico.Executar(
                    LogDocumento.AcaoAdicionaPagina, 
                    documentoFinal.Id, 
                    string.Format("Página [{0}] [{1}] foi adicionada ao documento na unificação.", pagina.Ordem, pagina.Id));

                documentoFinal.Paginas.Add(pagina);
            }
        }
    }
}
