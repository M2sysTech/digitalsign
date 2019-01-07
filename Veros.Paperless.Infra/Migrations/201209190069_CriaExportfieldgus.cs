namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190069)]
    public class CriaExportfieldgus : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "EXPORTFIELDGUS",
                this.WithId("EXPORTFIELDGUS_CODE"),
                new Column("EXPORTFIELD_ORDER", DbType.Int32, ColumnProperty.NotNull),
                new Column("TDCAMPOS_DESCEXPORT", DbType.AnsiString, 127),
                new Column("EXPORTFIELD_FORMAT", DbType.AnsiString, 127),
                new Column("EXPORTFIELD_VALORFIXO", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_EXPORTFIELDGUS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("EXPORTFIELDGUS");
            this.Database.RemoveSequence("SEQ_EXPORTFIELDGUS");
        }
    }
}
