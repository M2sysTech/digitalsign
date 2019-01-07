namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201308131442)]
    public class AdicionaTFimXmlNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_TFIMXML", DbType.Date));

            var sql = @"ALTER TABLE BATCH_BK ADD (BATCH_TFIMXML DATE)";

            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "BATCH_TFIMXML");

            var sql = @"ALTER TABLE BATCH_BK DROP COLUMN BATCH_TFIMXML";

            this.Database.ExecuteNonQuery(sql);
        }
    }
}
