namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190126)]
    public class CriaMotivo : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MOTIVO",
                this.WithId("MOTIVO_CODE"),
                new Column("MOTIVO_DESC", DbType.AnsiString, 254, ColumnProperty.NotNull),
                new Column("MOTIVO_TYPE", DbType.Int32));

            this.Database.CreateSequence("SEQ_MOTIVO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MOTIVO");
            this.Database.RemoveSequence("SEQ_MOTIVO");
        }
    }
}
