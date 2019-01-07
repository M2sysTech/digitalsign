namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190004)]
    public class CriaAccessModuleweb : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ACCESS_MODULEWEB",
                this.WithId("ACCESS_CODE"),
                new Column("EQUIPE_CODE", DbType.Int32, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_ACCESS_MODULEWEB");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ACCESS_MODULEWEB");
            this.Database.RemoveSequence("SEQ_ACCESS_MODULEWEB");
        }
    }
}
