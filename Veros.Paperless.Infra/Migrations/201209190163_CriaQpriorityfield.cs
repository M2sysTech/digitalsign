namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190163)]
    public class CriaQpriorityfield : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "QPRIORITYFIELD",
                this.WithId("QPRIORITYFIELD_CODE"),
                new Column("QPRIORITYFIELD_QTDFILA", DbType.Int32),
                new Column("TDCAMPOS_CODE", DbType.Int32),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("QPRIORITY_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("QPRIORITYFIELD_ORDER", DbType.Int32));

            this.Database.CreateSequence("SEQ_QPRIORITYFIELD");
        }

        public override void Down()
        {
            this.Database.RemoveTable("QPRIORITYFIELD");
            this.Database.RemoveSequence("SEQ_QPRIORITYFIELD");
        }
    }
}
