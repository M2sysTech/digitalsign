namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190103)]
    public class CriaLogpend : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGPEND",
                this.WithId("LOGPEND_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("DOC_CODE", DbType.Int32),
                new Column("TRANS_CODE", DbType.Int32),
                new Column("LOGPEND_DATETIME", DbType.DateTime),
                new Column("LOGPEND_MOTIVO", DbType.AnsiString, 127),
                new Column("LOGPEND_PV", DbType.AnsiString, 4),
                new Column("LOGPEND_DESC", DbType.AnsiString, 25));

            this.Database.CreateSequence("SEQ_LOGPEND");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGPEND");
            this.Database.RemoveSequence("SEQ_LOGPEND");
        }
    }
}
