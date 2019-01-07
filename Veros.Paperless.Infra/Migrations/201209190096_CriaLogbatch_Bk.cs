namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190096)]
    public class CriaLogbatchBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGBATCH_BK",
                this.WithId("LOGBATCH_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("BATCH_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("LOGBATCH_DATE", DbType.DateTime, ColumnProperty.NotNull),
                new Column("LOGBATCH_ACTION", DbType.AnsiString, 3, ColumnProperty.NotNull),
                new Column("LOGBATCH_OBS", DbType.AnsiString, 254));

            this.Database.CreateSequence("SEQ_LOGBATCH_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGBATCH_BK");
            this.Database.RemoveSequence("SEQ_LOGBATCH_BK");
        }
    }
}
