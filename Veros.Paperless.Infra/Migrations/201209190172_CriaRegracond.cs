namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190172)]
    public class CriaRegracond : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "REGRACOND",
                this.WithId("REGRACOND_CODE"),
                new Column("REGRA_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("REGRACOND_BINARIO", DbType.Int32),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("REGRA_CODEATENDIDO", DbType.Int32),
                new Column("TDCAMPOS_CODE", DbType.Int32),
                new Column("TDCAMPOS_COLUNA", DbType.AnsiString, 20),
                new Column("REGRACOND_COMPARACAO", DbType.AnsiString, 1),
                new Column("REGRACOND_VALORFIXO", DbType.AnsiString, 30),
                new Column("REGRACOND_OPERLOGICO", DbType.AnsiString, 2),
                new Column("TDCAMPOS_CODECOMPARAR", DbType.Int32),
                new Column("TDCAMPOS_COLUNACOMPARAR", DbType.AnsiString, 20),
                new Column("REGRACOND_OPERMATEMATICO", DbType.AnsiString, 2),
                new Column("REGRACOND_FATORMATEMATICO", DbType.AnsiString, 30),
                new Column("REGRACOND_DESC", DbType.AnsiString, 250),
                new Column("REGRACOND_ORDER", DbType.Int32));

            this.Database.CreateSequence("SEQ_REGRACOND");
        }

        public override void Down()
        {
            this.Database.RemoveTable("REGRACOND");
            this.Database.RemoveSequence("SEQ_REGRACOND");
        }
    }
}
