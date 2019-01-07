namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190181)]
    public class CriaRng : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "RNG",
                this.WithId("RNG_CODE"),
                new Column("RNG_COMBINACAO", DbType.AnsiString, 8),
                new Column("RNG_TRATAMENTO", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_RNG");
        }

        public override void Down()
        {
            this.Database.RemoveTable("RNG");
            this.Database.RemoveSequence("SEQ_RNG");
        }
    }
}
