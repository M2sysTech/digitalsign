namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class RegraAcao : Entidade
    {
        public virtual Regra Regra
        {
            get; 
            set;
        }

        public virtual TipoOrigem TipoOrigem
        {
            get;
            set;
        }

        public virtual ColunaDestino ColunaOrigem
        {
            get;
            set;
        }

        public virtual ColunaDestino ColunaDestino
        {
            get;
            set;
        }

        public virtual Campo Origem
        {
            get;
            set;
        }

        public virtual Campo Destino
        {
            get;
            set;
        }

        public virtual string ValorFixo
        {
            get;
            set;
        }
    }
}