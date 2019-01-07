namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190068)]
    public class CriaExportfield : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "EXPORTFIELD",
                this.WithId("EXPORTFIELD_CODE"),
                new Column("EXPORTCONFIG_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("EXPORTFIELD_ORDER", DbType.Int32, ColumnProperty.NotNull),
                new Column("TDCAMPOS_DESCEXPORT", DbType.AnsiString, 127),
                new Column("EXPORTFIELD_FORMAT", DbType.AnsiString, 127),
                new Column("EXPORTFIELD_VALORFIXO", DbType.AnsiString, 1),
                new Column("TDCAMPOS_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_EXPORTFIELD");
        }

        public override void Down()
        {
            this.Database.RemoveTable("EXPORTFIELD");
            this.Database.RemoveSequence("SEQ_EXPORTFIELD");
        }
    }
}
