namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201401161702)]
    public class CriaEstoquePortal : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ESTOQUEPORTAL",
                this.WithId("ESTOQUEPORTAL_CODE"),
                new Column("QUANTIDADE", DbType.Int32),
                new Column("ATUALIZADOEM", DbType.DateTime));

            this.Database.CreateSequence("SEQ_ESTOQUEPORTAL");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ESTOQUEPORTAL");
            this.Database.RemoveSequence("SEQ_ESTOQUEPORTAL");
        }
    }
}
