namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class DetalheRegra
    {
        public virtual int RegraId
        {
            get;
            set;
        }

        public virtual string Descricao
        {
            get;
            set;
        }

        public virtual int DocumentoId
        {
            get;
            set;
        }

        public virtual string Cpf
        {
            get;
            set;
        }

        public virtual bool Violada
        {
            get;
            set;
        }

        public virtual string TipoDeDocumento
        {
            get;
            set;
        }

        public int Sequencial
        {
            get;
            set;
        }

        public virtual string CodigoParticipante
        {
            get;
            set;
        }

        public virtual string Identificador
        {
            get;
            set;
        }

        public virtual string Status
        {
            get;
            set;
        }

        public virtual string Classificacao
        {
            get; 
            set;
        }
    }
}
