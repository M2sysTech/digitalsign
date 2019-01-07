namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190136)]
    public class CriaParbkp : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PARBKP",
                this.WithId("PARBKP_CODE"),
                new Column("PARBKP_DIR1", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("PARBKP_FREQ1", DbType.Int32),
                new Column("PARBKP_PER1", DbType.Int32),
                new Column("PARBKP_PER2", DbType.Int32),
                new Column("PARBKP_LAST1", DbType.DateTime),
                new Column("PARBKP_DIR2", DbType.AnsiString, 127),
                new Column("PARBKP_HORAEXEC", DbType.Int32),
                new Column("PARBKP_LAST2", DbType.DateTime),
                new Column("PARBKP_DIR3", DbType.AnsiString, 127),
                new Column("PARBKP_FREQ2", DbType.Int32),
                new Column("PARBKP_LAST3", DbType.DateTime),
                new Column("PARBKP_LASTDOC", DbType.Int32));

            this.Database.CreateSequence("SEQ_PARBKP");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PARBKP");
            this.Database.RemoveSequence("SEQ_PARBKP");
        }
    }
}
