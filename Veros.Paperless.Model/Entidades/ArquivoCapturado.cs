namespace Veros.Paperless.Model.Entidades
{
    public class ArquivoCapturado
    {
        public string Img
        {
            get;
            set;
        }

        public string Tipo
        {
            get;
            set;
        }

        public string TipoId
        {
            get;
            set;
        }

        public bool DocumentoFicha()
        {
            return this.TipoId == "35";
        }

        public bool EhFrente()
        {
            return this.Img.ToUpper().IndexOf("F.", System.StringComparison.Ordinal) > 0;
        }
    }
}