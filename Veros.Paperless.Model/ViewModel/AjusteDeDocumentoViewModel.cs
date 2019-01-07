namespace Veros.Paperless.Model.ViewModel
{
    using Entidades;

    public class AjusteDeDocumentoViewModel
    {
        public int DocumentoId { get; set; }

        public int Pagina { get; set; }

        public string Acao { get; set; }

        public int TipoDocumentoId { get; set; }

        public AjusteDeDocumento ToEntidade()
        {
            TipoDocumento tipoDocumento = null;
            
            if (this.TipoDocumentoId > 0)
            {
                tipoDocumento = new TipoDocumento { Id = this.TipoDocumentoId };
            }

            return new AjusteDeDocumento
            {
                Pagina = this.Pagina,
                Acao = AcaoAjusteDeDocumento.FromValue(this.Acao),
                Documento = new Documento { Id = this.DocumentoId },
                TipoDocumentoNovo = tipoDocumento
            };
        }
    }
}
