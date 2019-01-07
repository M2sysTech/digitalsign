namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201212031916)]
    public class CriaPacoteProcessado : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PACOTEPROCESSADO",
                this.WithId("PACOTEPROCESSADO_CODE"),
                new Column("PACOTEPROCESSADO_ARQUIVO", DbType.AnsiString, 100),
                new Column("PACOTEPROCESSADO_RECEBIDOEM", DbType.DateTime),
                new Column("PACOTEPROCESSADO_IMPORTADOEM", DbType.DateTime),
                new Column("PACOTEPROCESSADO_TOTALCONTAS", DbType.Int32));
            this.Database.CreateSequence("SEQ_PACOTEPROCESSADO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PACOTEPROCESSADO");
            this.Database.RemoveSequence("SEQ_PACOTEPROCESSADO");
        }
    }
}