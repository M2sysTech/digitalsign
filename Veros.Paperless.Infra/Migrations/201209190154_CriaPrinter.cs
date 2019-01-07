namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190154)]
    public class CriaPrinter : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PRINTER",
                this.WithId("PRINTER_CODE"),
                new Column("PRINTER_ALIAS", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("PRINTER_NAME", DbType.AnsiString, 127, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_PRINTER");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PRINTER");
            this.Database.RemoveSequence("SEQ_PRINTER");
        }
    }
}
