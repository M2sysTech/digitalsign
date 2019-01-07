namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201710191052)]
    public class CriaFileTransfer : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "FILETRANSFER",
                this.WithId("FILETRANSFER_CODE"),
                new Column("FILETRANSFER_TAG", DbType.AnsiString, 50),
                new Column("FILETRANSFER_SIZE", DbType.Double),
                new Column("FILETRANSFER_USED", DbType.Double));

            this.Database.CreateSequence("SEQ_FILETRANSFER");
        }

        public override void Down()
        {
            this.Database.RemoveTable("FILETRANSFER");
            this.Database.RemoveSequence("SEQ_FILETRANSFER");
        }
    }
}
