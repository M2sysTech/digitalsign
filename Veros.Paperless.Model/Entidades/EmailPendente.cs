namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;
    using System;

    public class EmailPendente : Entidade
    {
        public virtual string De
        {
            get; 
            set;
        }

        public virtual string Para
        {
            get; 
            set;
        }

        public virtual TipoNotificacao TipoNotificacao
        {
            get; 
            set;
        }

        public virtual Lote Lote
        {
            get; 
            set;
        }

        public virtual EmailPendenteStatus Status
        {
            get; 
            set;
        }

        public virtual Processo Processo
        {
            get;
            set;
        }

        public virtual DateTime? Dt
        {
            get;
            set;
        }

        public virtual int EnviaEm
        {
            get;
            set;
        }

        public virtual string Descricao
        {
            get;
            set;
        }
    }
}