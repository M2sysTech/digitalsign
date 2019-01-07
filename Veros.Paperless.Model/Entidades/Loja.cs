namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    [Serializable]
    public class Loja : Entidade
    {
        public virtual string Nome
        {
            get; 
            set;
        }
    }
}
