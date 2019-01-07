namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201401171134)]
    public class CriaRecepcao : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "recepcao",
                this.WithId("recepcao_code"),
                new Column("cadastrado_em", DbType.DateTime),
                new Column("usr_code", DbType.Int32),
                new Column("qtde_importado", DbType.Int32),
                new Column("status", DbType.Byte),
                new Column("finalizado_em", DbType.DateTime));

            this.Database.CreateSequence("seq_recepcao");
        }

        public override void Down()
        {
            this.Database.RemoveTable("recepcao");
            this.Database.RemoveSequence("seq_recepcao");
        }
    }
}
