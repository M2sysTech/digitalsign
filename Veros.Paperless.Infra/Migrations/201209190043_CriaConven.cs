namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190043)]
    public class CriaConven : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CONVEN",
                this.WithId("CONVEN_CODE"),
                new Column("CONVEN_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("CONVEN_ID", DbType.AnsiString, 16, ColumnProperty.NotNull),
                new Column("CONVEN_ARRECAD", DbType.AnsiString, 16),
                new Column("CONVEN_AGENC", DbType.AnsiString, 16),
                new Column("CONVEN_CENTR", DbType.AnsiString, 16),
                new Column("CONVEN_PRGCAD", DbType.AnsiString, 16),
                new Column("CONVEN_OBS", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_CONVEN");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CONVEN");
            this.Database.RemoveSequence("SEQ_CONVEN");
        }
    }
}
