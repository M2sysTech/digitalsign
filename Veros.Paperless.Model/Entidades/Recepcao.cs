namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Veros.Framework.Modelo;

    public class Recepcao : Entidade
    {
        public virtual RecepcaoStatus Status
        {
            get;
            set;
        }

        public virtual DateTime? FinalizadoEm
        {
            get;
            set;
        }

        public virtual int QuantidadeImportado
        {
            get;
            set;
        }

        public virtual DateTime CadastradoEm
        {
            get;
            set;
        }

        public virtual int UsuarioId
        {
            get;
            set;
        }

        public virtual int QuantidadeSolicitado
        {
            get;
            set;
        }

        public virtual bool Finalizado
        {
            get
            {
                return this.QuantidadeSolicitado == this.QuantidadeImportado;
            }
        }

        public virtual int QuantidadeParaSolicitar()
        {
            //// TODO: se quantidade solicitado for muito grande, deve quebrar em pequenas partes
            return this.QuantidadeSolicitado - this.QuantidadeImportado;
        }
    }
}