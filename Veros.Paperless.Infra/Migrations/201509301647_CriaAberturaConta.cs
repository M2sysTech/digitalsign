namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201509301647)]
    public class CriaAberturaConta : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ABERTURACONTA",
                this.WithId("ABERTURACONTA_CODE"),
                new Column("ABERTURACONTA_ID", DbType.String, 254),
                new Column("ABERTURACONTA_RECEBIDOEM", DbType.DateTime),
                new Column("ABERTURACONTA_STATUS", DbType.Int32));

            this.Database.CreateSequence("SEQ_ABERTURACONTA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ABERTURACONTA");
            this.Database.RemoveSequence("SEQ_ABERTURACONTA");
        }
    }
}
