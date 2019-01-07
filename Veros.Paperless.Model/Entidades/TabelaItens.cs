namespace Veros.Paperless.Model.Entidades
{
    using Veros.Framework.Modelo;

    public class TabelaItens : Entidade
    {
        public const string OrgaoEmissor = "DOMINIO_RG";

        public virtual string Identificador
        {
            get; 
            set;
        }

        public virtual string Chave
        {
            get; 
            set;
        }

        public virtual string Descricao
        {
            get;
            set;
        }

        public virtual int Codigo
        {
            get;
            set;
        }
    }
}