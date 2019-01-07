namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190020)]
    public class CriaBanco : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "BANCO",
                this.WithId("BANCO_CODE"),
                new Column("BANCO_NUM", DbType.AnsiString, 3, ColumnProperty.NotNull),
                new Column("BANCO_DV", DbType.AnsiString, 1),
                new Column("BANCO_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("BANCO_OBS", DbType.AnsiString, 4000),
                new Column("BANCO_ST", DbType.AnsiString, 1),
                new Column("BANCO_CONVENIADO", DbType.AnsiString, 1),
                new Column("BANCO_DBID", DbType.AnsiString, 3));

            this.Database.CreateSequence("SEQ_BANCO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("BANCO");
            this.Database.RemoveSequence("SEQ_BANCO");
        }
    }
}
