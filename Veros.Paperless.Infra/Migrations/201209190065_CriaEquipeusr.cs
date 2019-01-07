namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190065)]
    public class CriaEquipeusr : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "EQUIPEUSR",
                this.WithId("EQUIPEUSR_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_EQUIPEUSR");
        }

        public override void Down()
        {
            this.Database.RemoveTable("EQUIPEUSR");
            this.Database.RemoveSequence("SEQ_EQUIPEUSR");
        }
    }
}
