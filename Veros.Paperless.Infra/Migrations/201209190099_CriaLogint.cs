namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190099)]
    public class CriaLogint : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGINT",
                this.WithId("LOGINT_CODE"),
                new Column("LOGINT_DATETIME", DbType.DateTime, ColumnProperty.NotNull),
                new Column("USR_CODE", DbType.Int32),
                new Column("LOGINT_ENVINFO", DbType.AnsiString, 16),
                new Column("BATCH_CODE", DbType.Int32),
                new Column("TRANS_CODE", DbType.Int32),
                new Column("DOC_CODE", DbType.Int32),
                new Column("LOGINT_OBS", DbType.AnsiString, 254));

            this.Database.CreateSequence("SEQ_LOGINT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGINT");
            this.Database.RemoveSequence("SEQ_LOGINT");
        }
    }
}
