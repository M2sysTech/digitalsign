namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class TratamentoRegra : Entidade
    {
        public virtual Regra Regra
        {
            get;
            set;
        }

        public virtual TipoDocumento TipoDocumentoViolado
        {
            get;
            set;
        }

        public virtual int PaginaDocumentoViolado
        {
            get;
            set;
        }

        public virtual TipoDocumento TipoDocumentoComparado
        {
            get;
            set;
        }

        public virtual int PaginaDocumentoComparacao
        {
            get;
            set;
        }
    }
}