namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    [Serializable]
    public class Transportadora : Entidade
    {
        public const string StatusAtivo = "0";
        public const string StatusExcluido = "*";

        public virtual string Nome
        {
            get;
            set;
        }

        public virtual string Cnpj
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