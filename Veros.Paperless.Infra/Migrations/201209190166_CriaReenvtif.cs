namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190166)]
    public class CriaReenvtif : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "REENVTIF",
                this.WithId("REENVTIF_CODE"),
                new Column("DOC_CODE", DbType.Int32),
                new Column("REENVTIF_DT", DbType.DateTime),
                new Column("REENVTIF_USR", DbType.AnsiString, 127),
                new Column("REENVTIF_PV", DbType.AnsiString, 4));

            this.Database.CreateSequence("SEQ_REENVTIF");
        }

        public override void Down()
        {
            this.Database.RemoveTable("REENVTIF");
            this.Database.RemoveSequence("SEQ_REENVTIF");
        }
    }
}
