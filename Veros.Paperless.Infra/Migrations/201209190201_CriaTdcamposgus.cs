namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190201)]
    public class CriaTdcamposgus : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TDCAMPOSGUS",
                this.WithId("TDCAMPOSGUS_CODE"),
                new Column("TDCAMPOS_ID", DbType.Int32, ColumnProperty.NotNull),
                new Column("TYPEDOC_ID", DbType.Int32, ColumnProperty.NotNull),
                new Column("TDCAMPOS_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("TDCAMPOS_POSIMG", DbType.AnsiString, 127),
                new Column("TDCAMPOS_POSDIG", DbType.AnsiString, 127),
                new Column("TDCAMPOS_MARCA", DbType.AnsiString, 1),
                new Column("TDCAMPOS_MASCARA", DbType.AnsiString, 127),
                new Column("TDCAMPOS_DUPLADIGIT", DbType.AnsiString, 1),
                new Column("TDCAMPOS_PAGINA", DbType.Int32),
                new Column("TDCAMPOS_DIGITAVEL", DbType.AnsiString, 1),
                new Column("TDCAMPOS_COMPLEMENTO", DbType.AnsiString, 4000),
                new Column("TDCAMPOS_OBRIGATORIO", DbType.AnsiString, 1),
                new Column("TDCAMPOS_POSLIVRE", DbType.AnsiString, 1),
                new Column("TDCAMPOS_DESCEXPORT", DbType.AnsiString, 127),
                new Column("TDCAMPOS_PERCRECON", DbType.Int32));

            this.Database.CreateSequence("SEQ_TDCAMPOSGUS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TDCAMPOSGUS");
            this.Database.RemoveSequence("SEQ_TDCAMPOSGUS");
        }
    }
}
