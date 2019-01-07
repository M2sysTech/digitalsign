namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190177)]
    public class CriaRemessa : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "REMESSA",
                this.WithId("REMESSA_CODE"),
                new Column("PROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("REMESSA_STATUS", DbType.AnsiString, 2, ColumnProperty.NotNull),
                new Column("REMESSA_DATA", DbType.DateTime, ColumnProperty.NotNull),
                new Column("REMESSA_SEQUENCIAL", DbType.Int32, ColumnProperty.NotNull),
                new Column("REMESSA_DTGERACAO", DbType.DateTime),
                new Column("REMESSA_DTENVIO", DbType.DateTime),
                new Column("REMESSA_TENTENVIO", DbType.Int32),
                new Column("REMESSA_DTRECIBO", DbType.DateTime),
                new Column("REMESSA_NAMEARQUIVO", DbType.AnsiString, 1024));

            this.Database.CreateSequence("SEQ_REMESSA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("REMESSA");
            this.Database.RemoveSequence("SEQ_REMESSA");
        }
    }
}
