namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201803231100)]
    public class AdicionaDatasLotecef : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("LOTECEF", new Column("LOTECEF_DTGERACERTIFIC", DbType.Date));
            this.Database.AddColumn("LOTECEF", new Column("LOTECEF_DTASSINACERTIFIC", DbType.Date));
            this.Database.AddColumn("LOTECEF", new Column("USR_GEROU", DbType.Int32));
        }

        public override void Down()
        {
        }
    }
}
