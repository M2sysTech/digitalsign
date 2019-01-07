namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class Endereco : Entidade
    {
        public virtual string Cep
        {
            get;
            set;
        }

        public virtual string Logradouro
        {
            get;
            set;
        }

        public virtual string Bairro
        {
            get;
            set;
        }

        public virtual string Cidade
        {
            get;
            set;
        }

        public virtual string Uf
        {
            get;
            set;
        }

        public virtual string Complemento
        {
            get; 
            set;
        }
    }
}
