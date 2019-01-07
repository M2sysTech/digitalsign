namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201401231859)]
    public class AdicionarUltimaTentativaEmPendRecepcao : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn(
                "pendenciarecepcao", 
                new Column("ultima_tentativa", DbType.DateTime));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("pendenciarecepcao", "ultima_tentativa");
        }
    }
}
