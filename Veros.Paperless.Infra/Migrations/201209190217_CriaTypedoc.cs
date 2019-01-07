namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190217)]
    public class CriaTypedoc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TYPEDOC",
                this.WithId("TYPEDOC_CODE"),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("TYPEDOC_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("TYPEDOC_QTDPAG", DbType.Int32),
                new Column("TYPEDOC_POSSUIVERSO", DbType.AnsiString, 1),
                new Column("TYPEDOC_CLASATIVAR", DbType.AnsiString, 1),
                new Column("TYPEDOC_CLASTAM", DbType.Int32),
                new Column("TYPEDOC_CLASPREF", DbType.AnsiString, 10),
                new Column("TYPEDOC_CLASSUF", DbType.AnsiString, 10),
                new Column("TYPEDOC_TEMPLATEFILE", DbType.AnsiString, 50),
                new Column("TYPEDOC_RESTRITO", DbType.AnsiString, 1),
                new Column("TYPEDOC_AUTOCROP", DbType.AnsiString, 1),
                new Column("TYPEDOC_ORIENTACAO", DbType.AnsiString, 1),
                new Column("TYPEDOC_PAC", DbType.Int32));

            this.Database.CreateSequence("SEQ_TYPEDOC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TYPEDOC");
            this.Database.RemoveSequence("SEQ_TYPEDOC");
        }
    }
}
