namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class DocumentoExpurgo : Entidade
    {
        public virtual int DocCode
        {
            get;
            set;
        }

        public virtual string TipoArquivo
        {
            get;
            set;
        }
    }
}
