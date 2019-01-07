namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201211141600)]
    public class CriaRegraImportada : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "REGRAIMP",
                this.WithId("REGRAIMP_CODE"),
                new Column("REGRAIMP_VINCULO", DbType.AnsiString, 20),
                new Column("REGRAIMP_STATUS", DbType.AnsiString, 10),
                new Column("PROC_CODE", DbType.Int32),
                new Column("MDOC_CODE", DbType.Int32));
            this.Database.CreateSequence("SEQ_REGRAIMP");
        }

        public override void Down()
        {
            this.Database.RemoveTable("REGRAIMP");
            this.Database.RemoveSequence("SEQ_REGRAIMP");
        }
    }
}
