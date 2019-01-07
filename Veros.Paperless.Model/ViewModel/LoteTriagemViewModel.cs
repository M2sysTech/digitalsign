namespace Veros.Paperless.Model.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;

    public class LoteTriagemViewModel
    {
        public const string FaseTriagem = "TRIAGEM";
        public const string FaseAjuste = "AJUSTE";

        public int LoteId { get; set; }

        public int ProcessoId { get; set; }

        public string IdentificacaoProcesso { get; set; }

        public string Caixa { get; set; }

        public int LoteCefId { get; set; }

        public DateTime? LoteCefData { get; set; }

        public int ColetaId { get; set; }

        public DateTime? ColetaData { get; set; }

        public string IdentificacaoDaColeta { get; set; }

        public string TipoDeProcessoDescricao { get; set; }

        public DateTime? DataCriacao { get; set; }

        public IList<DocumentoParaSeparacaoViewModel> Documentos { get; set; }

        public IList<Documento> DocumentosNovos { get; set; }

        public DateTime DataCaptura { get; set; }

        public string Fase { get; set; }

        public static LoteTriagemViewModel Criar(Processo processo, string fase)
        {
            return new LoteTriagemViewModel
            {
                ProcessoId = processo.Id,
                LoteId = processo.Lote.Id,
                IdentificacaoProcesso = processo.Identificacao,
                Caixa = processo.Lote.Pacote.Identificacao,
                TipoDeProcessoDescricao = processo.TipoDeProcesso.Descricao,
                DataCriacao = processo.Lote.DataCriacao,
                Documentos = new List<DocumentoParaSeparacaoViewModel>(),
                DocumentosNovos = new List<Documento>(),
                DataCaptura = processo.Lote.PacoteProcessado.ArquivoRecebidoEm.GetValueOrDefault(),
                Fase = fase,
                LoteCefId = processo.Lote.LoteCef == null ? 0 : processo.Lote.LoteCef.Id,
                LoteCefData = processo.Lote.LoteCef == null ? null : processo.Lote.LoteCef.DataFim,
                ColetaId = processo.Lote.Pacote.Coleta.Id,
                ColetaData = processo.Lote.Pacote.Coleta.DataCadastro                
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
            var pagina = this.ObterPagina(paginaId);
            var documentoId = pagina.DocumentoAtualId();

            foreach (var documento in this.Documentos)
            {
                if (documento.Id == documentoId)
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

        public bool EstaNaTriagem()
        {
            return this.Fase == LoteTriagemViewModel.FaseTriagem;
        }
    }
}
