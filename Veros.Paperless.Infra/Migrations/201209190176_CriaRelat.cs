namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190176)]
    public class CriaRelat : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "RELAT",
                this.WithId("RELAT_CODE"),
                new Column("RELAT_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("RELAT_ARQUIVO", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("RELAT_OBS", DbType.AnsiString, 4000),
                new Column("RELAT_TIPO", DbType.Int32));

            this.Database.CreateSequence("SEQ_RELAT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("RELAT");
            this.Database.RemoveSequence("SEQ_RELAT");
        }
    }
}
