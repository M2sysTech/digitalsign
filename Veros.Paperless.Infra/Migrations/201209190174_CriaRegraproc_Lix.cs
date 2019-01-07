namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190174)]
    public class CriaRegraprocLix : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "REGRAPROC_LIX",
                this.WithId("REGRAPROC_CODE"),
                new Column("PROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("REGRA_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("REGRACOND_BINARIO", DbType.Int32),
                new Column("REGRAPROC_STATUS", DbType.AnsiString, 2),
                new Column("USR_CODE", DbType.Int32),
                new Column("REGRAPROC_DTSTART", DbType.DateTime),
                new Column("REGRAPROC_OBS", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_REGRAPROC_LIX");
        }

        public override void Down()
        {
            this.Database.RemoveTable("REGRAPROC_LIX");
            this.Database.RemoveSequence("SEQ_REGRAPROC_LIX");
        }
    }
}
