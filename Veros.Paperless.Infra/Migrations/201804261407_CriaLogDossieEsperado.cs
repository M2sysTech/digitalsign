namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201804261407)]
    public class CriaLogDossieEsperado : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGDOSSIEESPERADO",
                this.WithId("LOGDOSSIEESPERADO_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("DOSSIEESPERADO_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("LOGDOSSIEESPERADO_DATETIME", DbType.DateTime, ColumnProperty.NotNull),
                new Column("LOGDOSSIEESPERADO_ACTION", DbType.AnsiString, 3, ColumnProperty.NotNull),
                new Column("LOGDOSSIEESPERADO_DESC", DbType.AnsiString, 254));

            this.Database.CreateSequence("SEQ_LOGDOSSIEESPERADO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGDOSSIEESPERADO");
            this.Database.RemoveSequence("SEQ_LOGDOSSIEESPERADO");
        }
    }
}
