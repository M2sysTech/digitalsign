namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190175)]
    public class CriaRegratrat : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "REGRATRAT",
                this.WithId("REGRATRAT_CODE"),
                new Column("REGRA_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("REGRATRAT_TIPOTELA", DbType.AnsiString, 1),
                new Column("TDCAMPOS_CODE", DbType.Int32),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("TYPEDOC_PAGINA", DbType.Int32),
                new Column("TDCAMPOS_CODECOMPARAR", DbType.Int32),
                new Column("TYPEDOC_IDCOMPARAR", DbType.Int32),
                new Column("TYPEDOC_PAGINACOMPARAR", DbType.Int32),
                new Column("TDCAMPOS_CODELISTEDIT", DbType.AnsiString, 200));

            this.Database.CreateSequence("SEQ_REGRATRAT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("REGRATRAT");
            this.Database.RemoveSequence("SEQ_REGRATRAT");
        }
    }
}
