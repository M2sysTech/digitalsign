namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class Tarja : Entidade
    {
        public virtual Documento Documento
        {
            get;
            set;
        }

        public virtual Campo Campo
        {
            get;
            set;
        }

        public virtual Pagina Pagina
        {
            get;
            set;
        }

        public virtual int QtdeRetangulos
        {
            get;
            set;
        }

        public virtual string PosicoesRetangulos
        {
            get;
            set;
        }
    }
}
