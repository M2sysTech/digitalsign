namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190060)]
    public class CriaDraft3 : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DRAFT3",
                this.WithId("DRAFT3_CODE"),
                new Column("DRAFT3_OPERACAO", DbType.AnsiString, 3),
                new Column("DRAFT3_QTDEDEB", DbType.Int32),
                new Column("DRAFT3_VRDEBITO", DbType.Decimal),
                new Column("DRAFT3_QTDECRED", DbType.Int32),
                new Column("DRAFT3_VRCREDITO", DbType.Decimal),
                new Column("USR_CODE", DbType.Int32),
                new Column("DRAFT3_SUBCODIGO", DbType.AnsiString, 6),
                new Column("DRAFT3_AGENCIA", DbType.AnsiString, 4),
                new Column("DRAFT3_TYPEDOC_ID", DbType.Int32));

            this.Database.CreateSequence("SEQ_DRAFT3");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DRAFT3");
            this.Database.RemoveSequence("SEQ_DRAFT3");
        }
    }
}
