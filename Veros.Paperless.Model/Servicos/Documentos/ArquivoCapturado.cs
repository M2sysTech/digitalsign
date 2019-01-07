namespace Veros.Paperless.Model.Servicos.Documentos
{
    using Entidades;
    using FrameworkLocal;

    public class ArquivoCapturado
    {
        public int TipoDocumentoId { get; set; }

        public string Cpf { get; set; }

        public int TipoSinistradoId { get; set; }

        public static ArquivoCapturado CriarArquivo(string[] parametros)
        {
            return new ArquivoCapturado
            {
                TipoDocumentoId = parametros[0].ToInt(TipoDocumento.CodigoNaoIdentificado),
                Cpf = parametros[1],
                TipoSinistradoId = parametros[2].ToInt(1)
            };
        }

        public string NomeArquivo()
        {
            return string.Format("{0}{1}{2}", this.TipoDocumentoId, this.Cpf, this.TipoSinistradoId);
        }

        public bool EhIgual(Documento documento)
        {
            return this.TipoDocumentoId == documento.TipoDocumento.Id &&
                this.Cpf == documento.Cpf &&
                this.TipoSinistradoId == documento.SequenciaTitular;
        }
    }
}
