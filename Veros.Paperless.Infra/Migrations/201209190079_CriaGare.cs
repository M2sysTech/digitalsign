namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190079)]
    public class CriaGare : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "GARE",
                this.WithId("GARE_CODE"),
                new Column("GARE_UF", DbType.AnsiString, 2, ColumnProperty.NotNull),
                new Column("GARE_CODREC", DbType.AnsiString, 4),
                new Column("GARE_DATAVENCIM", DbType.AnsiString, 1),
                new Column("GARE_IE", DbType.AnsiString, 2),
                new Column("GARE_CNPJ", DbType.AnsiString, 2),
                new Column("GARE_INSDIVATIV", DbType.AnsiString, 2),
                new Column("GARE_REFERENCIA", DbType.AnsiString, 2),
                new Column("GARE_NUMPARCELA", DbType.AnsiString, 2),
                new Column("GARE_VALORRECEI", DbType.AnsiString, 1),
                new Column("GARE_VALORJUROS", DbType.AnsiString, 1),
                new Column("GARE_VALORMULTA", DbType.AnsiString, 1),
                new Column("GARE_VALORACRES", DbType.AnsiString, 1),
                new Column("GARE_VALOROUTRO", DbType.AnsiString, 1),
                new Column("GARE_VALORTOTAL", DbType.AnsiString, 1),
                new Column("GARE_GRPRECEITA", DbType.AnsiString, 1),
                new Column("GARE_AUTENTIC", DbType.AnsiString, 1),
                new Column("GARE_QTDEVIAS", DbType.AnsiString, 1),
                new Column("GARE_TIPODOC", DbType.AnsiString, 3),
                new Column("GARE_ST", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_GARE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("GARE");
            this.Database.RemoveSequence("SEQ_GARE");
        }
    }
}
