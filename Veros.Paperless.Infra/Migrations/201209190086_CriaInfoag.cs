namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190086)]
    public class CriaInfoag : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "INFOAG",
                this.WithId("INFOAG_CODE"),
                new Column("AGENCIA_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("INFOAG_DTMOV", DbType.DateTime),
                new Column("INFOAG_OBS", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_INFOAG");
        }

        public override void Down()
        {
            this.Database.RemoveTable("INFOAG");
            this.Database.RemoveSequence("SEQ_INFOAG");
        }
    }
}
