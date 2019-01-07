namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190162)]
    public class CriaQpriority : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "QPRIORITY",
                this.WithId("QPRIORITY_CODE"),
                new Column("QPRIORITY_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("QPRIORITY_AUTO", DbType.AnsiString, 1),
                new Column("QPRIORITY_QTDFILA", DbType.Int32));

            this.Database.CreateSequence("SEQ_QPRIORITY");
        }

        public override void Down()
        {
            this.Database.RemoveTable("QPRIORITY");
            this.Database.RemoveSequence("SEQ_QPRIORITY");
        }
    }
}
