namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class AcessoMap : ClassMap<Acesso>
    {
        public AcessoMap()
        {
            this.Table("Acesso");
            this.Id(x => x.Id).Column("ACESSO_CODE").GeneratedBy.Native("SEQ_ACESSO");
            this.References(x => x.Perfil).Column("PERFIL_CODE");
            this.Map(x => x.Funcionalidade).Column("ACESSO_REGRANUM");
        }
    }
}
