namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190197)]
    public class CriaTabelaitens : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
               "TABELAITENS",
               new Column("TITENS_CODE", DbType.Int32),
               new Column("TITENS_ID", DbType.AnsiString, 127),
               new Column("TITENS_CHAVE", DbType.AnsiString, 127),
               new Column("TITENS_DESC", DbType.AnsiString, 150));

            this.Database.CreateSequence("SEQ_TABELAITENS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TABELAITENS");
            this.Database.RemoveSequence("SEQ_TABELAITENS");
        }
    }
}
