namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190232)]
    public class CriaValfat : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "VALFAT",
                this.WithId("VALFAT_CODE"),
                new Column("VALFAT_MODO", DbType.AnsiString, 1, ColumnProperty.NotNull),
                new Column("VALFAT_DOCPROCES", DbType.Decimal),
                new Column("VALFAT_DOCCAPTU", DbType.Decimal),
                new Column("VALFAT_DOCDIGIT", DbType.Decimal),
                new Column("VALFAT_DOCAUTEN", DbType.Decimal));

            this.Database.CreateSequence("SEQ_VALFAT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("VALFAT");
            this.Database.RemoveSequence("SEQ_VALFAT");
        }
    }
}
