namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190194)]
    public class CriaSolverso : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SOLVERSO",
                this.WithId("SOLVERSO_CODE"),
                new Column("DOC_CODE", DbType.Int32),
                new Column("SOLVERSO_DT", DbType.DateTime),
                new Column("SOLVERSO_USR", DbType.AnsiString, 127),
                new Column("SOLVERSO_PV", DbType.AnsiString, 4),
                new Column("SOLVERSO_FILETYPE", DbType.AnsiString, 16));

            this.Database.CreateSequence("SEQ_SOLVERSO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SOLVERSO");
            this.Database.RemoveSequence("SEQ_SOLVERSO");
        }
    }
}
