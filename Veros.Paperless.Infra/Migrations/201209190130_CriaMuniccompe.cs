namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190130)]
    public class CriaMuniccompe : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MUNICCOMPE",
                this.WithId("MUNICCOMPE_CODE"),
                new Column("COMPE_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("MUNICCOMPE_ST", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_MUNICCOMPE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MUNICCOMPE");
            this.Database.RemoveSequence("SEQ_MUNICCOMPE");
        }
    }
}
