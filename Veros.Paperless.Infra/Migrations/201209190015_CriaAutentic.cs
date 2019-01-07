namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190015)]
    public class CriaAutentic : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "AUTENTIC",
                this.WithId("AUTENTIC_CODE"),
                new Column("AGENCIA_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("AUTENTIC_TIPO", DbType.AnsiString, 16),
                new Column("AUTENTIC_STATION", DbType.AnsiString, 127),
                new Column("AUTENTIC_IDTERM", DbType.AnsiString, 16),
                new Column("AUTENTIC_SEQ", DbType.Int32),
                new Column("AUTENTIC_TRMK", DbType.AnsiString, 1),
                new Column("REGISTER_CODE", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_AUTENTIC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("AUTENTIC");
            this.Database.RemoveSequence("SEQ_AUTENTIC");
        }
    }
}
