namespace Veros.Paperless.Model.Entidades
{
    using System.Collections.Generic;
    using Framework.Modelo;

    public class DetalheLote
    {
        public DetalheLote()
        {
            this.Documentos = new List<DetalheDocumento>();
            this.Regras = new List<DetalheRegra>();
        }

        public virtual int ProcessoId
        {
            get;
            set;
        }

        public virtual string Identificacao
        {
            get; 
            set;
        }

        public virtual string Numero
        {
            get;
            set;
        }

        public virtual string ParecerDoBanco
        {
            get;
            set;
        }

        public virtual int DocumentoDiId
        {
            get;
            set;
        }

        public virtual int DocumentoComprovanteResidenciaId
        {
            get;
            set;
        }

        public virtual int DocumentoFotoId
        {
            get;
            set;
        }

        public virtual IList<DetalheDocumento> Documentos
        {
            get;
            set;
        }

        public virtual IList<DetalheRegra> Regras
        {
            get;
            set;
        }

        public virtual bool PossuiIndicioDeFraude
        {
            get;
            set;
        }

        public virtual int ContadorLiberadas
        {
            get;
            set;
        }

        public virtual int ContadorDevolvidas
        {
            get;
            set;
        }

        public virtual bool EstaEmAprovacao
        {
            get; 
            set;
        }
    }
}
