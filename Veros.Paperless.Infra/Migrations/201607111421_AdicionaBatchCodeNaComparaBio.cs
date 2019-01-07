namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201607111421)]
    public class AdicionaBatchCodeNaComparaBio : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("ComparaBio", new Column("Batch_Code", DbType.Int32));
            this.Database.AddColumn("ComparaBio_Bk", new Column("Batch_Code", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("ComparaBio", "Batch_Code");
            this.Database.RemoveColumn("ComparaBio_Bk", "Batch_Code");
        }
    }
}
