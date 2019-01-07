namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190038)]
    public class CriaCompe : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "COMPE",
                this.WithId("COMPE_CODE"),
                new Column("COMPE_NUM", DbType.AnsiString, 3, ColumnProperty.NotNull),
                new Column("COMPE_DESC", DbType.AnsiString, 127),
                new Column("COMPE_LOCAL", DbType.AnsiString, 1),
                new Column("COMPE_ST", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_COMPE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("COMPE");
            this.Database.RemoveSequence("SEQ_COMPE");
        }
    }
}
