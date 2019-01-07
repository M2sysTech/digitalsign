namespace Veros.Framework.Modelo
{
    public class EntidadeDicionario : Entidade
    {
        public virtual string Chave
        {
            get;
            set;
        }

        public virtual string Valor
        {
            get;
            set;
        }
    }
}