namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190165)]
    public class CriaRecvalue : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "RECVALUE",
                this.WithId("RECVALUE_CODE"),
                new Column("RECVALUE_CONFIDENCE", DbType.Int32),
                new Column("RECVALUE_TYPE", DbType.Int32),
                new Column("RECVALUE_FIELD", DbType.AnsiString, 255),
                new Column("DOC_CODE", DbType.Int32),
                new Column("RECVALUE_TEMPLATE", DbType.AnsiString, 255),
                new Column("TDCAMPOS_CODE", DbType.Int32),
                new Column("RECVALUE_LEFT", DbType.Int32),
                new Column("RECVALUE_RIGHT", DbType.Int32),
                new Column("RECVALUE_WIDTH", DbType.Int32),
                new Column("RECVALUE_HEIGHT", DbType.Int32),
                new Column("RECVALUE_TOP", DbType.Int32),
                new Column("RECVALUE_BOTTOM", DbType.Int32),
                new Column("RECVALUE_VALUE", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_RECVALUE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("RECVALUE");
            this.Database.RemoveSequence("SEQ_RECVALUE");
        }
    }
}
