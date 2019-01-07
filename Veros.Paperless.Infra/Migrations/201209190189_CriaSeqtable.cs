namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190189)]
    public class CriaSeqtable : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SEQTABLE",
                this.WithId("SEQTABLE_CODE"));

            this.Database.CreateSequence("SEQ_SEQTABLE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SEQTABLE");
            this.Database.RemoveSequence("SEQ_SEQTABLE");
        }
    }
}
