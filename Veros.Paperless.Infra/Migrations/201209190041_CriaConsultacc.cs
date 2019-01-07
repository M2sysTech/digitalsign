namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190041)]
    public class CriaConsultacc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CONSULTACC",
                this.WithId("CONSULTACC_CODE"),
                new Column("DOC_AGENCIA", DbType.AnsiString, 4, ColumnProperty.NotNull),
                new Column("DOC_OPERACAO", DbType.AnsiString, 1, ColumnProperty.NotNull),
                new Column("DOC_CONTA", DbType.AnsiString, 12, ColumnProperty.NotNull),
                new Column("DOC_HOST", DbType.AnsiString, 127),
                new Column("DOC_HORATX", DbType.DateTime));

            this.Database.CreateSequence("SEQ_CONSULTACC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CONSULTACC");
            this.Database.RemoveSequence("SEQ_CONSULTACC");
        }
    }
}
