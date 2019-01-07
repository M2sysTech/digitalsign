namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190139)]
    public class CriaPendetiqueta : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PENDETIQUETA",
                this.WithId("PENDETIQUETA_CODE"),
                new Column("BATCH_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("PROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("PENDETIQUETA_STATUS", DbType.AnsiString, 2, ColumnProperty.NotNull),
                new Column("ETIQUETA_NUM", DbType.AnsiString, 150, ColumnProperty.NotNull),
                new Column("PENDETIQUETA_STARTTIME", DbType.DateTime));

            this.Database.CreateSequence("SEQ_PENDETIQUETA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PENDETIQUETA");
            this.Database.RemoveSequence("SEQ_PENDETIQUETA");
        }
    }
}
