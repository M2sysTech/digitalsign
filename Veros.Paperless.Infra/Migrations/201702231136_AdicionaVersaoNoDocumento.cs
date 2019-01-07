namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201702231136)]
    public class AdicionaVersaoNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_TPARQUIVO", DbType.AnsiString, 55));
            this.Database.AddColumn("MDOC", new Column("MDOC_VERSAO", DbType.AnsiString, 55));
            this.Database.AddColumn("MDOC", new Column("MDOC_PAI", DbType.Int32));
            this.Database.AddColumn("DOC", new Column("DOC_VERSAO", DbType.AnsiString, 55));
            this.Database.AddColumn("DOC", new Column("DOC_PAI", DbType.Int32));

            this.Database.AddColumn("MDOC_BK", new Column("MDOC_TPARQUIVO", DbType.AnsiString, 55));
            this.Database.AddColumn("MDOC_BK", new Column("MDOC_VERSAO", DbType.AnsiString, 55));
            this.Database.AddColumn("MDOC_BK", new Column("MDOC_PAI", DbType.Int32));
            this.Database.AddColumn("DOC_BK", new Column("DOC_VERSAO", DbType.AnsiString, 55));
            this.Database.AddColumn("DOC_BK", new Column("DOC_PAI", DbType.Int32));

            this.Database.AddColumn("MDOC_HIST", new Column("MDOC_TPARQUIVO", DbType.AnsiString, 55));
            this.Database.AddColumn("MDOC_HIST", new Column("MDOC_VERSAO", DbType.AnsiString, 55));
            this.Database.AddColumn("MDOC_HIST", new Column("MDOC_PAI", DbType.Int32));
            this.Database.AddColumn("DOC_HIST", new Column("DOC_VERSAO", DbType.AnsiString, 55));
            this.Database.AddColumn("DOC_HIST", new Column("DOC_PAI", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOC", "MDOC_TPARQUIVO");
            this.Database.RemoveColumn("MDOC", "MDOC_VERSAO");
            this.Database.RemoveColumn("MDOC", "MDOC_PAI");
            this.Database.RemoveColumn("DOC", "DOC_VERSAO");
            this.Database.RemoveColumn("DOC", "DOC_PAI");

            this.Database.RemoveColumn("MDOC_BK", "MDOC_TPARQUIVO");
            this.Database.RemoveColumn("MDOC_BK", "MDOC_VERSAO");
            this.Database.RemoveColumn("MDOC_BK", "MDOC_PAI");
            this.Database.RemoveColumn("DOC_BK", "DOC_VERSAO");
            this.Database.RemoveColumn("DOC_BK", "DOC_PAI");

            this.Database.RemoveColumn("MDOC_HIST", "MDOC_TPARQUIVO");
            this.Database.RemoveColumn("MDOC_HIST", "MDOC_VERSAO");
            this.Database.RemoveColumn("MDOC_HIST", "MDOC_PAI");
            this.Database.RemoveColumn("DOC_HIST", "DOC_VERSAO");
            this.Database.RemoveColumn("DOC_HIST", "DOC_PAI");
        }
    }
}
