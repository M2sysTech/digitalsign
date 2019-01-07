namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class RegraViolada : Entidade
    {
        public virtual Processo Processo
        {
            get;
            set;
        }

        public virtual RegraVioladaStatus Status
        {
            get;
            set;
        }

        public virtual Regra Regra
        {
            get;
            set;
        }

        public virtual Documento Documento
        {
            get;
            set;
        }

        public virtual int SomaDoBinario
        {
            get;
            set;
        }

        public virtual string Observacao
        {
            get;
            set;
        }

        public virtual string Vinculo
        {
            get;
            set;
        }

        public virtual string CpfParticipante
        {
            get;
            set;
        }

        public virtual int SequencialDoTitular
        {
            get;
            set;
        }

        public virtual bool Revisada
        {
            get;
            set;
        }

        public virtual string Pagina
        {
            get;
            set;
        }

        public virtual DateTime? Hora
        {
            get;
            set;
        }

        public virtual Usuario Usuario
        {
            get;
            set;
        }

        public virtual bool EstaPendente()
        {
            if (this.Status == RegraVioladaStatus.Marcada)
            {
                return true;
            }

            return this.Status == RegraVioladaStatus.Pendente && this.Regra.Fase == Regra.FaseAprovacao;
        }
    }
}