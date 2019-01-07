namespace Veros.Paperless.Model.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;

    public class LoteParaSeparacaoViewModel
    {
        public int LoteId { get; set; }

        public int ProcessoId { get; set; }

        public string IdentificacaoProcesso { get; set; }

        public string Caixa { get; set; }

        public string TipoDeProcessoDescricao { get; set; }

        public DateTime? DataCriacao { get; set; }

        public IList<DocumentoParaSeparacaoViewModel> Documentos { get; set; }

        public IList<Documento> DocumentosNovos { get; set; }

        public DateTime DataCaptura { get; set; }

        public static LoteParaSeparacaoViewModel Criar(Processo processo)
        {
            return new LoteParaSeparacaoViewModel
            {
                ProcessoId = processo.Id,
                LoteId = processo.Lote.Id,
                IdentificacaoProcesso = processo.Identificacao,
                Caixa = processo.Lote.Pacote.Identificacao,
                TipoDeProcessoDescricao = processo.TipoDeProcesso.Descricao,
                DataCriacao = processo.Lote.DataCriacao,
                Documentos = new List<DocumentoParaSeparacaoViewModel>(),
                DocumentosNovos = new List<Documento>(),
                DataCaptura = processo.Lote.PacoteProcessado.ArquivoRecebidoEm.GetValueOrDefault()
            };
        }

        public PaginaParaSeparacaoViewModel ObterPagina(int paginaId)
        {
            var documento = this.Documentos.FirstOrDefault(x => x.Paginas.Any(pagina => pagina.Id == paginaId));

            if (documento == null)
            {
                return null;
            }

            return documento.Paginas.FirstOrDefault(x => x.Id == paginaId);
        }

        public IList<PaginaParaSeparacaoViewModel> ObterPaginas()
        {
            var lista = new List<PaginaParaSeparacaoViewModel>();

            foreach (var documento in this.Documentos)
            {
                lista.AddRange(documento.Paginas);
            }

            return lista;
        }

        public void AtualizarOrdemDeDocumentoNovo(DocumentoParaSeparacaoViewModel documento)
        {
            var documentoNovo = this.DocumentosNovos.FirstOrDefault(x => x.Id == documento.Id);

            if (documentoNovo == null)
            {
                return;
            }

            documento.Ordem = documento.NovaOrdem;
        }

        public void MarcaDocumentoManipulado(int documentoId)
        {
            var documento = this.Documentos.FirstOrDefault(x => x.Id == documentoId);
            if (documento == null)
            {
                return;
            }

            documento.Manipulado = true;
        }

        public DocumentoParaSeparacaoViewModel ObterDocumentoDaPagina(int paginaId)
        {
            foreach (var documento in this.Documentos)
            {
                if (documento.Paginas.Any(x => x.Id == paginaId))
                {
                    return documento;
                }
            }

            return null;
        }

        public IList<int> ObterDocumentosAlterados()
        {
            var documentos = new List<int>();

            documentos.AddRange(this.DocumentosNovos.Select(x => x.Id));

            foreach (var documento in this.Documentos.Where(x => x.Manipulado))
            {
                documentos.Add(documento.Id);
            }

            return documentos.Distinct().ToList();
        }
    }
}
