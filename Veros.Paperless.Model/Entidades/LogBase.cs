namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public abstract class LogBase : Entidade
    {
        protected LogBase()
        {
            this.DataRegistro = DateTime.Now;
        }
        
        public virtual Usuario Usuario { get; set; }

        public virtual string Acao { get; set; }

        public virtual string Observacao { get; set; }

        public virtual DateTime DataRegistro { get; set; }
    }
}