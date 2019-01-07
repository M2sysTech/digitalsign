namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class RegraImportada : Entidade
    {
        public virtual Processo Processo
        {
            get;
            set;
        }

        public virtual Documento Documento
        {
            get;
            set;
        }

        public virtual string Vinculo 
        {
            get;
            set;
        }

        public virtual string Status
        {
            get;
            set;
        }
    }
}