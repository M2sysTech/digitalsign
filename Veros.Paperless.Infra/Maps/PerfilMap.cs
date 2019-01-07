namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class PerfilMap : ClassMap<Perfil>
    {
        public PerfilMap()
        {
            this.Table("Perfil");
            this.Id(x => x.Id).Column("PERFIL_CODE").GeneratedBy.Native("SEQ_PERFIL");
            this.Map(x => x.Descricao).Column("PERFIL_DESC");
            this.Map(x => x.Sigla).Column("PERFIL_SIGLA");

            this.HasMany(x => x.Acessos)
                .Inverse()
                .KeyColumn("PERFIL_CODE")
                .Cascade.None();
        }
    }
}
