namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class InstituicaoDeEnsinoMap : ClassMap<InstituicaoDeEnsino>
    {
        public InstituicaoDeEnsinoMap()
        {
            this.Table("INSTITUICAOENSINO");
            this.Id(x => x.Id).Column("INSTITUICAOENSINO_CODE").GeneratedBy.Assigned();
            this.Map(x => x.Nome).Column("INSTITUICAOENSINO_NAME");
        }
    }
}