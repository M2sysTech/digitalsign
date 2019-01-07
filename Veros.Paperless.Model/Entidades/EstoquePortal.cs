namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Veros.Framework.Modelo;

    public class EstoquePortal : EntidadeUnica
    {
        public virtual int Quantidade
        {
            get;
            set;
        }

        public virtual DateTime AtualizadoEm
        {
            get;
            set;
        }
    }
}