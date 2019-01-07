namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class ConfiguracaoRemessaCampo : Entidade
    {
        public virtual string NomeExibicao
        {
            get;
            set;
        }

        public virtual string Ordem
        {
            get;
            set;
        }

        public virtual string ValorFixo
        {
            get;
            set;
        }

        public virtual Campo Campo
        {
            get;
            set;
        }

        public virtual ConfiguracaoRemessa ConfiguracaoRemessa
        {
            get;
            set;
        }

        public virtual string Mascara
        {
            get;
            set;
        }
    }
}
