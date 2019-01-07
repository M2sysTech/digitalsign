namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201401171424)]
    public class AdicionaSolicitadoEmRecepcao : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("recepcao", "qtde_solicitado", DbType.Int32);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("recepcao", "qtde_solicitado");
        }
    }
}
