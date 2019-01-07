namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class EnderecoMap : ClassMap<Endereco>
    {
        public EnderecoMap()
        {
            this.Table("CEPBRASIL");
            this.Id(x => x.Id).Column("CEP_CODE");
            this.Map(x => x.Cep).Column("CEP_NUMERO");
            this.Map(x => x.Bairro).Column("CEP_BAIRRO");
            this.Map(x => x.Cidade).Column("CEP_CIDADE");
            this.Map(x => x.Logradouro).Column("CEP_LOGRADOURO");
            this.Map(x => x.Uf).Column("CEP_UF");
            this.Map(x => x.Complemento).Column("CEP_COMPLEMENTO");
        }
    }
}
