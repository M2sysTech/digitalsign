namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190124)]
    public class CriaModuleweb : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MODULEWEB",
                this.WithId("MODULEWEB_CODE"),
                new Column("MODULEWEB_DESC", DbType.AnsiString, 256),
                new Column("MODULEWEB_NIVEL", DbType.Int32),
                new Column("MODULEWEB_REF", DbType.AnsiString, 128),
                new Column("MODULEWEB_ORDER", DbType.Int32));

            this.Database.CreateSequence("SEQ_MODULEWEB");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MODULEWEB");
            this.Database.RemoveSequence("SEQ_MODULEWEB");
        }
    }
}
