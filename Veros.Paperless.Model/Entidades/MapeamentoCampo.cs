namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class MapeamentoCampo : Entidade
    {
        public virtual Campo Campo
        {
            get;
            set;
        }

        public virtual string NomeTemplate
        {
            get;
            set;
        }

        public virtual string NomeCampoNoTemplate
        {
            get;
            set;
        }
    }
}
