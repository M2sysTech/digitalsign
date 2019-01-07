namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201311041000)]
    public class AdicionaCpfNaMdoc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_CPF", DbType.AnsiString, 11));
            this.Database.AddColumn("MDOC", new Column("MDOC_SEQTIT", DbType.Int16));
            this.Database.AddColumn("MDOC_BK", new Column("MDOC_CPF", DbType.AnsiString, 11));
            this.Database.AddColumn("MDOC_BK", new Column("MDOC_SEQTIT", DbType.Int16));
            this.Database.AddColumn("MDOC_HIST", new Column("MDOC_CPF", DbType.AnsiString, 11));
            this.Database.AddColumn("MDOC_HIST", new Column("MDOC_SEQTIT", DbType.Int16));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOC", "MDOC_CPF");
            this.Database.RemoveColumn("MDOC", "MDOC_SEQTIT");
            this.Database.RemoveColumn("MDOC_BK", "MDOC_CPF");
            this.Database.RemoveColumn("MDOC_BK", "MDOC_SEQTIT");
            this.Database.RemoveColumn("MDOC_HIST", "MDOC_CPF");
            this.Database.RemoveColumn("MDOC_HIST", "MDOC_SEQTIT");
        }
    }
}