namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class Acesso : Entidade
    {
        public virtual Perfil Perfil
        {
            get;
            set;
        }

        public virtual Funcionalidade Funcionalidade
        {
            get;
            set;
        }
    }
}
