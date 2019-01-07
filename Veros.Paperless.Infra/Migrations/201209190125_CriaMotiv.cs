namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190125)]
    public class CriaMotiv : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MOTIV",
                this.WithId("MOTIV_CODE"),
                new Column("MOTIV_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("MOTIV_TIPO", DbType.Int32));

            this.Database.CreateSequence("SEQ_MOTIV");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MOTIV");
            this.Database.RemoveSequence("SEQ_MOTIV");
        }
    }
}
