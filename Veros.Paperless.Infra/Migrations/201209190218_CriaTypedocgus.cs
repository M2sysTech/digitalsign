namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190218)]
    public class CriaTypedocgus : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TYPEDOCGUS",
                this.WithId("TYPEDOCGUS_CODE"),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("TYPEDOC_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("TYPEDOC_QTDPAG", DbType.Int32),
                new Column("TYPEDOC_POSSUIVERSO", DbType.AnsiString, 1),
                new Column("TYPEDOC_CLASATIVAR", DbType.AnsiString, 1),
                new Column("TYPEDOC_CLASTAM", DbType.Int32),
                new Column("TYPEDOC_CLASPREF", DbType.AnsiString, 10),
                new Column("TYPEDOC_CLASSUF", DbType.AnsiString, 10));

            this.Database.CreateSequence("SEQ_TYPEDOCGUS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TYPEDOCGUS");
            this.Database.RemoveSequence("SEQ_TYPEDOCGUS");
        }
    }
}
