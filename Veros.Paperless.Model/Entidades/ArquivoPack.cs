namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class ArquivoPack : Entidade
    {
        public virtual PacoteProcessado PacoteProcessado
        {
            get;
            set;
        }

        public virtual DateTime Data
        {
            get;
            set;
        }

        public virtual string NomePacote
        {
            get;
            set;
        }

        public virtual long Tamanho
        {
            get;
            set;
        }

        public virtual ArquivoPackStatus Status
        {
            get;
            set;
        }

        public virtual string Observacao
        {
            get;
            set;
        }
    }
}