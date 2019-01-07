namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class Remessa : Entidade
    {
        public Remessa()
        {
            this.DataHoraGeracao = DateTime.Now;
        }

        public virtual string Arquivo
        {
            get;
            set;
        }

        public virtual string Extensao
        {
            get;
            set;
        }

        public virtual DateTime DataHoraGeracao
        {
            get;
            set;
        }
        
        public virtual DateTime DataHoraRecibo
        {
            get;
            set;
        }
        
        public virtual DateTime DataHoraRemessa
        {
            get;
            set;
        }
        
        public virtual RemessaStatus Status
        {
            get;
            set;
        }

        public virtual Processo Processo
        {
            get;
            set;
        }

        public virtual int Sequencial
        {
            get;
            set;
        }
    }
}
