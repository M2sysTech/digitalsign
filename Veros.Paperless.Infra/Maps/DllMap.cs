namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class DllMap : ClassMap<Dll>
    {
        public DllMap()
        {
            this.Table("DLL");
            this.Id(x => x.Id).Column("DLL_CODE").GeneratedBy.Native("SEQ_DLL");
            this.Map(x => x.Name).Column("DLL_NAME");
            this.Map(x => x.Version).Column("DLL_VERSION");
            this.Map(x => x.Restart).Column("DLL_RESTART");
            this.Map(x => x.Overwrite).Column("DLL_OVERWRITE");
            this.Map(x => x.Usage).Column("DLL_USAGE");
        }
    }
}
