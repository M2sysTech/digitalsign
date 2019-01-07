namespace Veros.Paperless.Model.ViewModel
{
    using Entidades;

    public class PaginaParaSeparacaoViewModel
    {
        public int Id { get; set; }

        public int DataCenterId { get; set; }

        public int DocumentoId { get; set; }

        public int OrdemNoBanco { get; set; }

        public int Ordem { get; set; }

        public PaginaStatus Status { get; set; }

        public string TipoArquivo { get; set; }

        public string Removido { get; set; }

        public int NovoDocumentoId { get; set; }

        public string NomeArquivo { get; set; }

        public int DocumentoPaiNaTela { get; set; }

        public bool Recapturado { get; set; }

        public static PaginaParaSeparacaoViewModel Criar(Pagina pagina, int ordem)
        {
            var paginaExcluida = "N";

            if (pagina.Status == PaginaStatus.StatusExcluida || pagina.Documento.TipoDocumento.Id == TipoDocumento.CodigoDocumentoGeral)
            {
                paginaExcluida = "S";
            }

            return new PaginaParaSeparacaoViewModel
            {
                Id = pagina.Id,
                DataCenterId = pagina.DataCenter,
                Ordem = ordem,
                OrdemNoBanco = pagina.Ordem,
                Status = pagina.Status,
                DocumentoId = pagina.Documento.Id,
                TipoArquivo = pagina.TipoArquivo,
                NomeArquivo = pagina.NomeArquivoSemExtensao,
                Removido = paginaExcluida,
                Recapturado = pagina.Recapturado == "S"
            };
        }

        public int DocumentoAtualId()
        {
            return this.NovoDocumentoId > 0 ? this.NovoDocumentoId : this.DocumentoId;
        }
    }
}