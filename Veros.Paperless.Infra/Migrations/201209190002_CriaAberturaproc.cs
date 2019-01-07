namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190002)]
    public class CriaAberturaproc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ABERTURAPROC",
                this.WithId("ABERTURAPROC_CODE"),
                new Column("TYPEPROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("ABERTURAPROC_IDENTIFICADOR", DbType.AnsiString, 254),
                new Column("ABERTURAPROC_DADOS", DbType.AnsiString, 4000),
                new Column("ABERTURAPROC_DOCS", DbType.AnsiString, 4000),
                new Column("ABERTURAPROC_STATUS", DbType.AnsiString, 1, ColumnProperty.NotNull),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("ABERTURAPROC_DTCAD", DbType.DateTime, ColumnProperty.NotNull),
                new Column("AGENCIA_NUM", DbType.AnsiString, 5),
                new Column("ABERTURAPROC_FLUXO", DbType.AnsiString, 4000),
                new Column("BATCH_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_ABERTURAPROC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ABERTURAPROC");
            this.Database.RemoveSequence("SEQ_ABERTURAPROC");
        }
    }
}
