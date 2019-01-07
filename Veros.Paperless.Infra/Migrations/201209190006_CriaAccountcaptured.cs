namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190006)]
    public class CriaAccountcaptured : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ACCOUNTCAPTURED",
                this.WithId("ACCOUNTCAPTURED_CODE"),
                new Column("ACCOUNTCAPTURED_CPF", DbType.AnsiString, 20));

            this.Database.CreateSequence("SEQ_ACCOUNTCAPTURED");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ACCOUNTCAPTURED");
            this.Database.RemoveSequence("SEQ_ACCOUNTCAPTURED");
        }
    }
}
