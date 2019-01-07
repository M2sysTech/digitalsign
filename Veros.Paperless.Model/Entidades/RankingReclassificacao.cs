namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class RankingReclassificacao : Entidade
    {
        public virtual TipoDocumento TipoDocumento
        {
            get;
            set;
        }

        public virtual int Quantidade
        {
            get;
            set;
        }

        public virtual void IncrementarRank(int valor)
        {
            this.Quantidade += valor;
        }
    }
}
