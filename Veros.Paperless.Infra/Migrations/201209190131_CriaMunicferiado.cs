namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190131)]
    public class CriaMunicferiado : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MUNICFERIADO",
                this.WithId("MUNICFERIADO_CODE"),
                new Column("FERIADO_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("MUNICFERIADO_ST", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_MUNICFERIADO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MUNICFERIADO");
            this.Database.RemoveSequence("SEQ_MUNICFERIADO");
        }
    }
}
