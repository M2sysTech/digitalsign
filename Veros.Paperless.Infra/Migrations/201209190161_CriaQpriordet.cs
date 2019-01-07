namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190161)]
    public class CriaQpriordet : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "QPRIORDET",
                this.WithId("QPRIORDET_CODE"),
                new Column("QPRIORITY_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("QPRIORDET_ORDER", DbType.Int32),
                new Column("TYPEDOC_ID", DbType.Int32));

            this.Database.CreateSequence("SEQ_QPRIORDET");
        }

        public override void Down()
        {
            this.Database.RemoveTable("QPRIORDET");
            this.Database.RemoveSequence("SEQ_QPRIORDET");
        }
    }
}
