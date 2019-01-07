namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190105)]
    public class CriaLogproc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGPROC",
                this.WithId("LOGPROC_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("PROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("LOGPROC_DATE", DbType.DateTime, ColumnProperty.NotNull),
                new Column("LOGPROC_ACTION", DbType.AnsiString, 3, ColumnProperty.NotNull),
                new Column("LOGPROC_OBS", DbType.AnsiString, 254));

            this.Database.CreateSequence("SEQ_LOGPROC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGPROC");
            this.Database.RemoveSequence("SEQ_LOGPROC");
        }
    }
}
