namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190075)]
    public class CriaFormalisticagus : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "FORMALISTICAGUS",
                this.WithId("FORMALISTICAGUS_CODE"),
                new Column("TYPEPROC_CODE", DbType.Int32),
                new Column("TYPEDOC_IMG", DbType.Int32, ColumnProperty.NotNull),
                new Column("TDCAMPOS_IMG", DbType.Int32, ColumnProperty.NotNull),
                new Column("TYPEDOC_INFO", DbType.Int32),
                new Column("TDCAMPOS_INFO", DbType.Int32),
                new Column("FORMALISTICA_TIPO", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_FORMALISTICAGUS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("FORMALISTICAGUS");
            this.Database.RemoveSequence("SEQ_FORMALISTICAGUS");
        }
    }
}
