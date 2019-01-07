namespace Veros.Paperless.Model.Entidades
{
    public class ImagemDoLote
    {
        public bool Verso
        {
            get;
            set;
        }

        public string NomeArquivo
        {
            get;
            set;
        }

        public string UrlArquivo
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public bool Enviado
        {
            get;
            set;
        }

        public string ConteudoEmBase64
        {
            get;
            set;
        }

        public string Hash
        {
            get;
            set;
        }

        public int LoteId
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }

        public bool Upado
        {
            get;
            set;
        }

        public string TipoDocumentoId
        {
            get;
            set;
        }
    }
}