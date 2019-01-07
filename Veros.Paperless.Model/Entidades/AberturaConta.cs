namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class AberturaConta : Entidade
    {
        public virtual string Identificacao
        {
            get; 
            set;
        }

        public virtual DateTime RecebidoEm
        {
            get; 
            set;
        }

        public virtual AberturaContaStatus Status
        {
            get; 
            set;
        }

        public virtual string Cpf
        {
            get; 
            set;
        }

        public virtual DateTime CpfRecebidoEm
        {
            get; 
            set;
        }
    }
}