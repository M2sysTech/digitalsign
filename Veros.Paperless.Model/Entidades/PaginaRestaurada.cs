namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class PaginaRestaurada : Entidade
    {
        public virtual Documento Documento
        {
            get;
            set;
        }

        public virtual Pagina Pagina
        {
            get;
            set;
        }
    }
}