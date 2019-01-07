namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190030)]
    public class CriaCctarifa : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CCTARIFA",
                this.WithId("CCTARIFA_CODE"),
                new Column("CCTARIFA_AGENCIA", DbType.AnsiString, 4, ColumnProperty.NotNull),
                new Column("CCTARIFA_OPER", DbType.AnsiString, 1),
                new Column("CCTARIFA_CONTA", DbType.AnsiString, 12, ColumnProperty.NotNull),
                new Column("CCTARIFA_STATUS", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_CCTARIFA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CCTARIFA");
            this.Database.RemoveSequence("SEQ_CCTARIFA");
        }
    }
}
