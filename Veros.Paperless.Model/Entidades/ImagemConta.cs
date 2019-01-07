namespace Veros.Paperless.Model.Entidades
{
    using FrameworkLocal;

    public class ImagemConta
    {   
        public string Cpf
        {
            get; 
            set;
        }

        public int TipoDocumentoId
        {
            get;
            set;
        }

        public string FormatoBase64
        {
            get;
            set;
        }

        public int Face
        {
            get; 
            set;
        }

        public string Ocr
        {
            get;
            set;
        }

        public string ObterTipoArquivo()
        {
            ////data:image/jpeg;base64,
            var posicaoDoisPontos = this.FormatoBase64.IndexOf(":", System.StringComparison.Ordinal) + 1;
            var posicaoPontoVirgula = this.FormatoBase64.IndexOf(";", System.StringComparison.Ordinal);

            var mime = this.FormatoBase64.Substring(
                posicaoDoisPontos,
                posicaoPontoVirgula - posicaoDoisPontos);

            return MimeTypes.GetExtension(mime);
        }

        public string ObterBase64()
        {
            ////data:image/jpeg;base64,(...)
            var posicaoVirgula = this.FormatoBase64.IndexOf(",", System.StringComparison.Ordinal) + 1;

            var base64 = this.FormatoBase64.Substring(posicaoVirgula);

            return base64;
        }
    }
}