namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190034)]
    public class CriaClassif : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CLASSIF",
                this.WithId("CLASSIF_CODE"),
                new Column("AGENCIA_NUM", DbType.AnsiString, 4),
                new Column("CLASSIF_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("CLASSIF_TIPOCLAS", DbType.AnsiString, 127),
                new Column("CLASSIF_COMPUTER", DbType.AnsiString, 127),
                new Column("CLASSIF_COMMPORT", DbType.Int32),
                new Column("CLASSIF_IDTERM", DbType.AnsiString, 16),
                new Column("CLASSIF_GATEWAY", DbType.AnsiString, 1),
                new Column("CLASSIF_TOTDOCS", DbType.Int32),
                new Column("CLASSIF_PARCDOCS", DbType.Int32),
                new Column("CLASSIF_DTMANUT", DbType.DateTime),
                new Column("CLASSIF_PARCDOC2", DbType.Int32),
                new Column("CLASSIF_DTMANUT2", DbType.DateTime),
                new Column("SCANNER_CODE", DbType.Int32),
                new Column("CLASSIF_LASTTX", DbType.DateTime),
                new Column("CLASSIF_DBID", DbType.AnsiString, 3),
                new Column("REGISTER_CODE", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_CLASSIF");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CLASSIF");
            this.Database.RemoveSequence("SEQ_CLASSIF");
        }
    }
}
