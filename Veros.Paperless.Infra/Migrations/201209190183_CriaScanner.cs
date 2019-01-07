namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190183)]
    public class CriaScanner : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SCANNER",
                this.WithId("SCANNER_CODE"),
                new Column("SCANNER_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("SCANNER_RESOLUC", DbType.Int32),
                new Column("SCANNER_ADF", DbType.AnsiString, 1),
                new Column("SCANNER_BITONAL", DbType.AnsiString, 1),
                new Column("SCANNER_DUPLEX", DbType.AnsiString, 1),
                new Column("SCANNER_DRIVER", DbType.AnsiString, 127),
                new Column("SCANNER_BRILHO", DbType.AnsiString, 127),
                new Column("SCANNER_CONTRAST", DbType.AnsiString, 127),
                new Column("SCANNER_BRILHO2", DbType.AnsiString, 127),
                new Column("SCANNER_CONTRAS2", DbType.AnsiString, 127),
                new Column("SCANNER_AUTOBRI", DbType.AnsiString, 1),
                new Column("SCANNER_DRVCONFIG", DbType.Int32),
                new Column("SCANNER_MAXMANROT", DbType.Int32),
                new Column("SCANNER_MAXMANCOM", DbType.Int32),
                new Column("SCANNER_DBID", DbType.AnsiString, 3));

            this.Database.CreateSequence("SEQ_SCANNER");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SCANNER");
            this.Database.RemoveSequence("SEQ_SCANNER");
        }
    }
}
