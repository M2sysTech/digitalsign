namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190095)]
    public class CriaLogbatch : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGBATCH",
                this.WithId("LOGBATCH_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("BATCH_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("LOGBATCH_DATE", DbType.DateTime, ColumnProperty.NotNull),
                new Column("LOGBATCH_ACTION", DbType.AnsiString, 3, ColumnProperty.NotNull),
                new Column("LOGBATCH_OBS", DbType.AnsiString, 254));

            this.Database.CreateSequence("SEQ_LOGBATCH");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGBATCH");
            this.Database.RemoveSequence("SEQ_LOGBATCH");
        }
    }
}
