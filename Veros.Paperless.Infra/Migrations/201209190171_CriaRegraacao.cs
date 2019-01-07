namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190171)]
    public class CriaRegraacao : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "REGRAACAO",
                this.WithId("REGRAACAO_CODE"),
                new Column("REGRA_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("REGRAACAO_STATUSAPROVAR", DbType.AnsiString, 20),
                new Column("REGRAACAO_STATUSMARCAR", DbType.AnsiString, 20),
                new Column("REGRAACAO_TIPOORIGEM", DbType.Int32),
                new Column("REGRAACAO_CODEORIGEM", DbType.Int32),
                new Column("REGRAACAO_COLUNAORIGEM", DbType.Int32),
                new Column("REGRAACAO_VALORFIXO", DbType.AnsiString, 30),
                new Column("REGRAACAO_CODEDESTINO", DbType.Int32),
                new Column("REGRAACAO_COLUNADESTINO", DbType.Int32));

            this.Database.CreateSequence("SEQ_REGRAACAO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("REGRAACAO");
            this.Database.RemoveSequence("SEQ_REGRAACAO");
        }
    }
}
