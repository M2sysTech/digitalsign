namespace Veros.Paperless.Model.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;

    public class DocumentoViewModel
    {
        private readonly string codigoAlatorioInterno = string.Empty;

        public DocumentoViewModel()
        {
            this.codigoAlatorioInterno = DateTime.Now.ToString("mmssfff");
        }

        public int DocumentoId { get; set; }

        public int DocumentoPaiId { get; set; }

        public int LoteId { get; set; }

        public int TipoId { get; set; }

        public string TipoDescricao { get; set; }

        public string TipoArquivo { get; set; }

        public string Versao { get; set; }

        public string Cpf { get; set; }

        public bool Virtual { get; set; }

        public bool Excluido { get; set; }

        public string MarcaDeNovaVersao { get; set; }

        public int CodigoDoPai { get;  set; }

        public string Observacao { get; set; }

        public DateTime DataCaptura { get; set; }

        public string CodigoAleatorio
        {
            get
            {
                return this.codigoAlatorioInterno;
            }
        }

        public DossieViewModel Processo { get; set; }

        public IList<Pagina> Paginas { get; set; }

        public static DocumentoViewModel Criar(Documento documento, bool carregarPaginas = false)
        {
            var documentoViewModel = new DocumentoViewModel
            {
                DocumentoId = documento.Id,
                LoteId = documento.Lote.Id,
                TipoId = documento.TipoDocumento.Id,
                TipoDescricao = documento.TipoDocumento.Description,
                TipoArquivo = documento.TipoDeArquivo,
                Cpf = documento.Cpf,
                Versao = documento.Versao,
                MarcaDeNovaVersao = documento.Templates,
                Virtual = documento.Virtual,
                CodigoDoPai = documento.DocumentoPaiId,
                DataCaptura = documento.Lote.PacoteProcessado.ArquivoRecebidoEm.GetValueOrDefault(),
                Excluido = documento.Status == DocumentoStatus.Excluido,
                DocumentoPaiId = documento.DocumentoPaiId
            };

            if (carregarPaginas)
            {
                documentoViewModel.Paginas = documento.ObterPaginasOrdenadas().ToList();
            }

            return documentoViewModel;
        }

        public static IList<DocumentoViewModel> CriarPorLista(IList<Documento> documentos)
        {
            var lista = new List<DocumentoViewModel>();

            foreach (var documento in documentos)
            {
                lista.Add(DocumentoViewModel.Criar(documento));
            }

            return lista;
        }
    }
}
