namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class IndiceOcr : Entidade
    {
        public virtual int Acertos
        {
            get;
            set;
        }

        public virtual int Erros
        {
            get;
            set;
        }

        public virtual TipoDocumento TipoDocumento
        {
            get;
            set;
        }

        public virtual Campo Campo
        {
            get;
            set;
        }

        public virtual DateTime Data
        {
            get;
            set;
        }
    }
}
