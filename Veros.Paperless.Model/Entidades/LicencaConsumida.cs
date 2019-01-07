namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class LicencaConsumida : Entidade
    {
        public virtual Pagina Pagina
        {
            get;
            set;
        }

        public virtual int Quantidade
        {
            get;
            set;
        }
    }
}