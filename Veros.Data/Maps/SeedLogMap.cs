namespace Veros.Data.Maps
{
    using Entities;
    using FluentNHibernate.Mapping;

    public class SeedLogMap : ClassMap<SeedLog>
    {
        public SeedLogMap()
        {
            this.Table("seedlog");
            this.Id(x => x.Id).Column("id").GeneratedBy.Native("seq_seedlog");
            this.Map(x => x.Name).Column("name");
            this.Map(x => x.Version).Column("version");
            this.Map(x => x.AppVersion).Column("appversion");
            this.Map(x => x.ExecutedDate).Column("executed_at");
            this.Map(x => x.Machine).Column("machine");
        }
    }
}