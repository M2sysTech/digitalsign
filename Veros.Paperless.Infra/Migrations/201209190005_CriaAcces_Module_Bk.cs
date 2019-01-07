namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190005)]
    public class CriaAccesModuleBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ACCES_MODULE_BK",
                this.WithId("ACCES_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("ACCESS_MODULE_DBID", DbType.AnsiString, 3));

            this.Database.CreateSequence("SEQ_ACCES_MODULE_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ACCES_MODULE_BK");
            this.Database.RemoveSequence("SEQ_ACCES_MODULE_BK");
        }
    }
}
