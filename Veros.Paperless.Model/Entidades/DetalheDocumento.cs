namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class DetalheDocumento
    {
        public virtual int DocumentoId
        {
            get;
            set;
        }

        public virtual int TipoId
        {
            get;
            set;
        }

        public virtual string Tipo
        {
            get;
            set;
        }

        public virtual string Cpf
        {
            get;
            set;
        }

        public virtual string Paginas
        {
            get;
            set;
        }

        public virtual bool Mestre
        {
            get;
            set;
        }

        public virtual string IndicioDeFraude
        {
            get;
            set;
        }

        public virtual int Sequencial
        {
            get;
            set;
        }

        public virtual string CodigoParticipante
        {
            get;
            set;
        }
    }
}