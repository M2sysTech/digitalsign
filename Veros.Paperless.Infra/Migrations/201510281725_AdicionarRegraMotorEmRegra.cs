namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201510281725)]
    public class AdicionarRegraMotorEmRegra : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn(
                "regra", 
                new Column("regra_processarmotor", DbType.AnsiString, 1));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("regra", "regra_processarmotor");
        }
    }
}
