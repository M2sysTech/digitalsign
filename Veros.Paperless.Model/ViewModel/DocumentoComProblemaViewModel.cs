namespace Veros.Paperless.Model.ViewModel
{
    using Entidades;

    public class DocumentoComProblemaViewModel
    {
        public int ProcessoId { get; set; }

        public int DocumentoId { get; set; }

        public string Pagina { get; set; }

        public string TipoProblema { get; set; }

        public int RegraId { get; set; }

        public string DescricaoOutroProblema { get; set; } 

        public Documento ObterDocumento()
        {
            return this.DocumentoId > 0 ? 
                new Documento { Id = this.DocumentoId } : 
                null;
        }

        public Processo ObterProcesso()
        {
            return this.ProcessoId > 0 ?
                new Processo { Id = this.ProcessoId } :
                null;
        }

        public string TipoDeProblemaCompleto()
        {
            if (this.TipoProblema == "Outro problema")
            {
                return string.Format("{0}: {1}", this.TipoProblema, this.DescricaoOutroProblema);
            }

            return this.TipoProblema;
        }

        public string TipoProblemaComPagina()
        {
            return string.Format("{0} - Pagina: {1}", this.TipoProblema, this.Pagina);
        }
    }
}
