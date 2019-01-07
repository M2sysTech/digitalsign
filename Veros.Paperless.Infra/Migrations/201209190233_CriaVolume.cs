namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190233)]
    public class CriaVolume : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "VOLUME",
                this.WithId("VOLUME_CODE"),
                new Column("VOLUME_DATE", DbType.DateTime, ColumnProperty.NotNull),
                new Column("VOLUME_ESPERADO", DbType.Int32),
                new Column("VOLUME_PROC", DbType.Int32),
                new Column("VOLUME_PROCACUM", DbType.Int32));

            this.Database.CreateSequence("SEQ_VOLUME");
        }

        public override void Down()
        {
            this.Database.RemoveTable("VOLUME");
            this.Database.RemoveSequence("SEQ_VOLUME");
        }
    }
}
