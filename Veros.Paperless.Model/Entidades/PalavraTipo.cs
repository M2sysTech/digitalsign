namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class PalavraTipo : Entidade
    {
        public virtual string Texto
        {
            get;
            set;
        }

        public virtual PalavraTipoCategoria Categoria
        {
            get;
            set;
        }

        public virtual TipoDocumento TipoDocumento
        {
            get;
            set;
        }
    }
}