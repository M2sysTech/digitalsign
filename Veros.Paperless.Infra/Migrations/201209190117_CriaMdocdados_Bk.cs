namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190117)]
    public class CriaMdocdadosBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MDOCDADOS_BK",
                this.WithId("MDOCDADOS_CODE"),
                new Column("MDOC_CODE", DbType.Int32),
                new Column("MDOCDADOS_VALOR1", DbType.AnsiString, 4000),
                new Column("MDOCDADOS_VALOR2", DbType.AnsiString, 4000),
                new Column("MDOCDADOS_VALOROK", DbType.AnsiString, 4000),
                new Column("MDOCDADOS_STATUSFORM", DbType.AnsiString, 3),
                new Column("TDCAMPOS_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_MDOCDADOS_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MDOCDADOS_BK");
            this.Database.RemoveSequence("SEQ_MDOCDADOS_BK");
        }
    }
}
