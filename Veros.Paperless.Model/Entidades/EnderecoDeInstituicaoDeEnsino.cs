namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class EnderecoDeInstituicaoDeEnsino : Entidade
    {
        public virtual string Descricao
        {
            get;
            set;
        }

        public virtual string Endereco
        {
            get;
            set;
        }

        public virtual string Cidade
        {
            get;
            set;
        }

        public virtual InstituicaoDeEnsino InstituicaoDeEnsino
        {
            get;
            set;
        }
    }
}