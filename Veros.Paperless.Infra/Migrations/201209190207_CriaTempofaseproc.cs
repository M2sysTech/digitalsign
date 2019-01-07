namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190207)]
    public class CriaTempofaseproc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TEMPOFASEPROC",
                this.WithId("TEMPOFASEPROC_CODE"),
                new Column("PROC_STATUS", DbType.AnsiString, 2, ColumnProperty.NotNull),
                new Column("TEMPOFASE_INI", DbType.DateTime, ColumnProperty.NotNull),
                new Column("TEMPOFASE_FIM", DbType.DateTime));

            this.Database.CreateSequence("SEQ_TEMPOFASEPROC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TEMPOFASEPROC");
            this.Database.RemoveSequence("SEQ_TEMPOFASEPROC");
        }
    }
}
