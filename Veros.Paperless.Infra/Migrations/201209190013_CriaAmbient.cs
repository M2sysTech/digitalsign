namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190013)]
    public class CriaAmbient : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "AMBIENT",
                this.WithId("AMBIENT_CODE"),
                new Column("AMBIENT_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("AMBIENT_REQAGENCIA", DbType.AnsiString, 1),
                new Column("AMBIENT_PERIODOFIM", DbType.DateTime),
                new Column("AMBIENT_PERIODOINICIO", DbType.DateTime, ColumnProperty.NotNull),
                new Column("AMBIENT_QTDESTACOES", DbType.Int32, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_AMBIENT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("AMBIENT");
            this.Database.RemoveSequence("SEQ_AMBIENT");
        }
    }
}
