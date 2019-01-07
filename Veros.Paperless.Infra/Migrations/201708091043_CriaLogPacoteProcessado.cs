namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201708091043)]
    public class CriaLogPacoteProcessado : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGPACOTEPROCESSADO",
                this.WithId("LOGPAC_CODE"),
                new Column("USR_CODE", DbType.Int32),
                new Column("PACOTEPROCESSADO_CODE", DbType.Int32),
                new Column("LOGPAC_DATE", DbType.Date),
                new Column("LOGPAC_ACTION", DbType.AnsiString, 3),
                new Column("LOGPAC_OBS", DbType.AnsiString, 1500));

            this.Database.CreateSequence("SEQ_LOGPACOTEPROCESSADO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGPACOTEPROCESSADO");
            this.Database.RemoveSequence("SEQ_LOGPACOTEPROCESSADO");
        }
    }
}
