namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190170)]
    public class CriaRegra : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "REGRA",
                this.WithId("REGRA_CODE"),
                new Column("REGRA_IDENTIFICADOR", DbType.AnsiString, 10, ColumnProperty.NotNull),
                new Column("REGRA_DESC", DbType.AnsiString, 250, ColumnProperty.NotNull),
                new Column("REGRA_ATIVADA", DbType.AnsiString, 1),
                new Column("REGRA_FASE", DbType.AnsiString, 1),
                new Column("REGRA_CONECLOGICO", DbType.AnsiString, 2),
                new Column("REGRA_AUTOINCREMET", DbType.AnsiString, 1),
                new Column("REGRA_CLASSIF", DbType.AnsiString, 1),
                new Column("TYPEPROC_ID", DbType.AnsiString, 5));

            this.Database.CreateSequence("SEQ_REGRA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("REGRA");
            this.Database.RemoveSequence("SEQ_REGRA");
        }
    }
}
