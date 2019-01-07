namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class LogBatimento : Entidade
    {
        public virtual Indexacao Indexacao
        {
            get;
            set;
        }

        public virtual string CampoEngine
        {
            get;
            set;
        }

        public virtual string ValorReconhecido
        {
            get;
            set;
        }
    }
}