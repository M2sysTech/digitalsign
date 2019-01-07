namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class Banco : Entidade
    {
        public virtual string Numero
        {
            get;
            set;
        }

        public virtual string Nome
        {
            get;
            set;
        }
    }
}
