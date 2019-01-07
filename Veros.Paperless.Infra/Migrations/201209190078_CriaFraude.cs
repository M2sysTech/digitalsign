namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190078)]
    public class CriaFraude : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "FRAUDE",
                this.WithId("FRAUDE_CODE"),
                new Column("FRAUDE_DUP", DbType.AnsiString, 1),
                new Column("FRAUDE_LAMIN", DbType.AnsiString, 1),
                new Column("FRAUDE_LAMINLIM", DbType.Decimal),
                new Column("FRAUDE_DUPVLRMIN", DbType.Decimal),
                new Column("FRAUDE_DUPCHK", DbType.AnsiString, 1),
                new Column("FRAUDE_DUPARR", DbType.AnsiString, 1),
                new Column("FRAUDE_DUPFIC", DbType.AnsiString, 1),
                new Column("FRAUDE_DUPENV", DbType.AnsiString, 1),
                new Column("FRAUDE_DUPOUT", DbType.AnsiString, 1),
                new Column("FRAUDE_DUPDEP", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_FRAUDE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("FRAUDE");
            this.Database.RemoveSequence("SEQ_FRAUDE");
        }
    }
}
