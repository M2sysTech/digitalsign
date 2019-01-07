namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Veros.Framework.Modelo;

    public class PendenciaPortal : Entidade
    {
        public virtual int SolicitacaoId
        {
            get;
            set;
        }

        public virtual int Tentativas
        {
            get;
            set;
        }

        public virtual string UltimoErro
        {
            get;
            set;
        }

        public virtual DateTime UltimaTentativa
        {
            get;
            set;
        }

        public virtual Operacao Operacao
        {
            get;
            set;
        }

        public virtual bool PassouTempoDeEspera
        {
            get
            {
                return DateTime.Now.Subtract(this.UltimaTentativa) > RecepcaoConfiguracao.IntervaloDeEspera;
            }
        }
    }
}