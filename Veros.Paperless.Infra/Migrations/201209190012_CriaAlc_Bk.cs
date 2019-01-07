namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190012)]
    public class CriaAlcBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ALC_BK",
                this.WithId("ALC_CODE"),
                new Column("TRANS_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("DOC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("ALC_OSF", DbType.AnsiString, 10),
                new Column("ALC_TIPO", DbType.Int32),
                new Column("ALC_MATRUSR", DbType.AnsiString, 8),
                new Column("ALC_MATRUSR2", DbType.AnsiString, 8),
                new Column("ALC_MATRUSR3", DbType.AnsiString, 8),
                new Column("ALC_PREPOS", DbType.AnsiString, 1),
                new Column("ALC_STATUS", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_ALC_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ALC_BK");
            this.Database.RemoveSequence("SEQ_ALC_BK");
        }
    }
}
