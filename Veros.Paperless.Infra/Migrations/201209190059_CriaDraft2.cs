namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190059)]
    public class CriaDraft2 : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DRAFT2",
                this.WithId("DRAFT2_CODE"),
                new Column("DRAFT2_DTMOV", DbType.DateTime),
                new Column("DRAFT2_HREMIS", DbType.AnsiString, 8),
                new Column("DRAFT2_PV", DbType.AnsiString, 127),
                new Column("DRAFT2_OPERARACAO", DbType.AnsiString, 16),
                new Column("DRAFT2_EVENTO", DbType.AnsiString, 8),
                new Column("DRAFT2_QTDE", DbType.AnsiString, 3),
                new Column("DRAFT2_VALOR", DbType.Decimal),
                new Column("DRAFT2_DEBCRD", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_DRAFT2");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DRAFT2");
            this.Database.RemoveSequence("SEQ_DRAFT2");
        }
    }
}
