namespace Veros.Data.Maps
{
    using Entities;
    using FluentNHibernate.Mapping;

    public class SeedInfoMap : ClassMap<SeedInfo>
    {
        public SeedInfoMap()
        {
            this.Table("seedinfo");
            this.Id(x => x.Id).Column("id").GeneratedBy.Native("seq_seedinfo");
            this.Map(x => x.Name).Column("name");
            this.Map(x => x.Version).Column("version");
            this.Map(x => x.AppVersion).Column("appversion");
            this.Map(x => x.ExecutedDate).Column("executed_at");
        }
    }
}