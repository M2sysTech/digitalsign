namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190003)]
    public class CriaAccessModule : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ACCESS_MODULE",
                this.WithId("ACCESS_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("ACCESS_MODULE_DBID", DbType.AnsiString, 3));

            this.Database.CreateSequence("SEQ_ACCESS_MODULE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ACCESS_MODULE");
            this.Database.RemoveSequence("SEQ_ACCESS_MODULE");
        }
    }
}
