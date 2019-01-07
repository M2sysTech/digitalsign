namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201706141348)]
    public class CriaAjusteDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "AJUSTEMDOC",
                this.WithId("AJUSTEMDOC_CODE"),
                new Column("MDOC_CODE", DbType.Int32),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("AJUSTEMDOC_PAG", DbType.Int32),
                new Column("AJUSTEMDOC_ACAO", DbType.AnsiString, 3),
                new Column("AJUSTEMDOC_STATUS", DbType.AnsiString, 3),
                new Column("AJUSTEMDOC_DTREGISTRO", DbType.Date),
                new Column("AJUSTEMDOC_DTFIM", DbType.Date),
                new Column("USR_CODE", DbType.Date));

            this.Database.CreateSequence("SEQ_AJUSTEMDOC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("AJUSTEMDOC");
            this.Database.RemoveSequence("SEQ_AJUSTEMDOC");
        }
    }
}
