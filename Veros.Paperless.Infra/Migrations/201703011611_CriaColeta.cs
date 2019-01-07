namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201703011611)]
    public class CriaColeta : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "COLETA",
                this.WithId("COLETA_CODE"),
                new Column("COLETA_DATA", DbType.Date, ColumnProperty.NotNull),
                new Column("COLETA_ENDERECO", DbType.AnsiString, 550),
                new Column("COLETA_DESCRICAO", DbType.AnsiString, 2500),
                new Column("COLETA_PERIODO", DbType.AnsiString, 155),
                new Column("COLETA_QTD1", DbType.Int32),
                new Column("COLETA_QTD2", DbType.Int32),
                new Column("USR_CAD", DbType.Int32),
                new Column("COLETA_DTCAD", DbType.Date),
                new Column("COLETA_STATUS", DbType.AnsiString, 3),
                new Column("USR_REALIZ", DbType.Int32),
                new Column("COLETA_DTREALIZ", DbType.Date));

            this.Database.CreateSequence("SEQ_COLETA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("COLETA");
            this.Database.RemoveSequence("SEQ_COLETA");
        }
    }
}
