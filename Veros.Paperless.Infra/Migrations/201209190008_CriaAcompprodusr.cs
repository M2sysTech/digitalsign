namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190008)]
    public class CriaAcompprodusr : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ACOMPPRODUSR",
                this.WithId("ACOMPPRODUSR_CODE"),
                new Column("TYPEDOC_ID", DbType.Int32, ColumnProperty.NotNull),
                new Column("ACOMPPRODUSR_TOQUES", DbType.Int32, ColumnProperty.NotNull),
                new Column("ACOMPPRODUSR_SEGS", DbType.Int32, ColumnProperty.NotNull),
                new Column("ACOMPPRODUSR_FASE", DbType.AnsiString, 3, ColumnProperty.NotNull),
                new Column("ACOMPPRODUSR_DATE", DbType.DateTime, ColumnProperty.NotNull),
                new Column("MDOC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("TDCAMPOS_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_ACOMPPRODUSR");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ACOMPPRODUSR");
            this.Database.RemoveSequence("SEQ_ACOMPPRODUSR");
        }
    }
}
