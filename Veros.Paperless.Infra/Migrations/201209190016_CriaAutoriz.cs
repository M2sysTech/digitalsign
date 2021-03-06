namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190016)]
    public class CriaAutoriz : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "AUTORIZ",
                this.WithId("AUTORIZ_CODE"),
                new Column("USR_CODE", DbType.Int32),
                new Column("AUTORIZ_APROVATR", DbType.AnsiString, 1),
                new Column("AUTORIZ_GERARDLE", DbType.AnsiString, 1),
                new Column("AUTORIZ_EDITAR", DbType.AnsiString, 1),
                new Column("AUTORIZ_EXCLDOCS", DbType.AnsiString, 1),
                new Column("AUTORIZ_CHAT", DbType.AnsiString, 1),
                new Column("AUTORIZ_FECHALOG", DbType.AnsiString, 1),
                new Column("AUTORIZ_LOGDOC", DbType.AnsiString, 1),
                new Column("AUTORIZ_CANCELTR", DbType.AnsiString, 1),
                new Column("AUTORIZ_VINCULAR", DbType.AnsiString, 1),
                new Column("AUTORIZ_EXCLUSAO", DbType.AnsiString, 1),
                new Column("AUTORIZ_MARCAR", DbType.AnsiString, 1),
                new Column("AUTORIZ_DBID", DbType.AnsiString, 3));

            this.Database.CreateSequence("SEQ_AUTORIZ");
        }

        public override void Down()
        {
            this.Database.RemoveTable("AUTORIZ");
            this.Database.RemoveSequence("SEQ_AUTORIZ");
        }
    }
}
