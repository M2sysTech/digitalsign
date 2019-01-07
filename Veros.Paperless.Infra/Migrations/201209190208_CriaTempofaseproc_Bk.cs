namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190208)]
    public class CriaTempofaseprocBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TEMPOFASEPROC_BK",
                this.WithId("TEMPOFASEPROC_CODE"),
                new Column("PROC_STATUS", DbType.AnsiString, 2),
                new Column("TEMPOFASE_INI", DbType.DateTime),
                new Column("TEMPOFASE_FIM", DbType.DateTime));

            this.Database.CreateSequence("SEQ_TEMPOFASEPROC_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TEMPOFASEPROC_BK");
            this.Database.RemoveSequence("SEQ_TEMPOFASEPROC_BK");
        }
    }
}
