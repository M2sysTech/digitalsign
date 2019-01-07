namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190035)]
    public class CriaCliente : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CLIENTE",
                this.WithId("CLIENTE_CODE"),
                new Column("CLIENTE_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("CLIENTE_SIGLA", DbType.AnsiString, 15, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_CLIENTE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CLIENTE");
            this.Database.RemoveSequence("SEQ_CLIENTE");
        }
    }
}
