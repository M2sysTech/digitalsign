namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190225)]
    public class CriaUsrequipe : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "USREQUIPE",
                this.WithId("USREQUIPE_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_USREQUIPE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("USREQUIPE");
            this.Database.RemoveSequence("SEQ_USREQUIPE");
        }
    }
}
