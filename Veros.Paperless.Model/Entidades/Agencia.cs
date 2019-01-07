namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class Agencia : Entidade
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

        public virtual int BancoId
        {
            get;
            set;
        }

        public virtual int MunicCode
        {
            get;
            set;
        }
    }
}
