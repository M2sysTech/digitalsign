namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201306191013)]
    public class CriaDocumentoExpurgo : Migration
    {
        public override void Up()
        {
            this.Database.AddTable("DOCEXPURGO",
                this.WithId("DOCEXPURGO_CODE"),
                new Column("DOCEXPURGO_CODEDOCUMENTO", DbType.Int32),
                new Column("DOCEXPURGO_TIPOARQUIVO", DbType.AnsiString, 50 ));

            this.Database.CreateSequence("SEQ_DOCEXPURGO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DOCEXPURGO");
            this.Database.RemoveSequence("SEQ_DOCEXPURGO");
        }
    }
}
