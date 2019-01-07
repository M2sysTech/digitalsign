namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class EnderecoDeInstituicaoDeEnsinoMap : ClassMap<EnderecoDeInstituicaoDeEnsino>
    {
        public EnderecoDeInstituicaoDeEnsinoMap()
        {
            this.Table("IEENDERECO");
            this.Id(x => x.Id).Column("IEENDERECO_CODE").GeneratedBy.Assigned();
            this.Map(x => x.Descricao).Column("IEENDERECO_DESC");
            this.References(x => x.InstituicaoDeEnsino).Column("INSTITUICAOENSINO_CODE");
        }
    }
}