namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class Template : Entidade
    {
        public virtual string Nome
        {
            get;
            set;
        }

        public virtual string Chaves
        {
            get;
            set;
        }

        public virtual int Ordem
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
