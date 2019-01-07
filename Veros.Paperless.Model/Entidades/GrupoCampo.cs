namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    [Serializable]
    public class GrupoCampo : Entidade
    {
        public virtual string Nome
        {
            get;
            set;
        }

        public virtual int Ordem
        {
            get;
            set;
        }
    }
}
