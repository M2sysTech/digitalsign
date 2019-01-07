namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190014)]
    public class CriaArr : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ARR",
                this.WithId("ARR_CODE"),
                new Column("ARR_SEGMENTO", DbType.Int32, ColumnProperty.NotNull),
                new Column("ARR_CONVENIO", DbType.Int32),
                new Column("ARR_UF", DbType.AnsiString, 2),
                new Column("ARR_DESC", DbType.AnsiString, 127),
                new Column("ARR_CEDENTE", DbType.Int32),
                new Column("ARR_DEBAUT", DbType.Int32),
                new Column("ARR_DIA", DbType.Int32),
                new Column("ARR_ABRANG", DbType.AnsiString, 2),
                new Column("ARR_FORMAREC", DbType.Int32),
                new Column("ARR_BARRAS", DbType.AnsiString, 1),
                new Column("ARR_ESPECIF", DbType.AnsiString, 1),
                new Column("ARR_CONVENC", DbType.AnsiString, 1),
                new Column("ARR_VALIDVCTO", DbType.Int32));

            this.Database.CreateSequence("SEQ_ARR");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ARR");
            this.Database.RemoveSequence("SEQ_ARR");
        }
    }
}
