namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190072)]
    public class CriaFeriadonac : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "FERIADONAC",
                this.WithId("FERIADONAC_CODE"),
                new Column("FERIADONAC_DATE", DbType.DateTime, ColumnProperty.NotNull),
                new Column("FERIADONAC_DESC", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_FERIADONAC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("FERIADONAC");
            this.Database.RemoveSequence("SEQ_FERIADONAC");
        }
    }
}
