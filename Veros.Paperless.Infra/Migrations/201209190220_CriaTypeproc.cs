namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190220)]
    public class CriaTypeproc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TYPEPROC",
                this.WithId("TYPEPROC_CODE"),
                new Column("TYPEPROC_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("TYPEPROC_CAPASEPARADORA", DbType.AnsiString, 1),
                new Column("TYPEPROC_REQUERAPROVACAO", DbType.AnsiString, 1),
                new Column("TYPEPROC_DESCRESUMO", DbType.AnsiString, 127),
                new Column("TYPEPROC_INFORMARCAIXA", DbType.AnsiString, 1),
                new Column("TYPEPROC_PRIORITY", DbType.Int32, ColumnProperty.NotNull),
                new Column("TYPEPROC_CAPTURAPARCIAL", DbType.AnsiString, 1, ColumnProperty.NotNull),
                new Column("TYPEPROC_ID", DbType.AnsiString, 5));

            this.Database.CreateSequence("SEQ_TYPEPROC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TYPEPROC");
            this.Database.RemoveSequence("SEQ_TYPEPROC");
        }
    }
}
