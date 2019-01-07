namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190150)]
    public class CriaPlanTable : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PLAN_TABLE",
                this.WithId("PLAN_CODE"),
                new Column("TIMESTAMP", DbType.DateTime),
                new Column("REMARKS", DbType.AnsiString, 127),
                new Column("OPERATION", DbType.AnsiString, 127),
                new Column("OPTIONS", DbType.AnsiString, 127),
                new Column("OBJECT_NODE", DbType.AnsiString, 127),
                new Column("OBJECT_OWNER", DbType.AnsiString, 127),
                new Column("OBJECT_NAME", DbType.AnsiString, 127),
                new Column("OBJECT_INSTANCE", DbType.Decimal),
                new Column("OBJECT_TYPE", DbType.AnsiString, 127),
                new Column("OPTIMIZER", DbType.AnsiString, 254),
                new Column("SEARCH_COLUMNS", DbType.Int32),
                new Column("ID", DbType.Decimal),
                new Column("PARENT_ID", DbType.Decimal),
                new Column("POSITION", DbType.Decimal),
                new Column("COST", DbType.Decimal),
                new Column("CARDINALITY", DbType.Decimal),
                new Column("BYTES", DbType.Decimal),
                new Column("OTHER_TAG", DbType.AnsiString, 254),
                new Column("PARTITION_START", DbType.AnsiString, 254),
                new Column("PARTITION_STOP", DbType.AnsiString, 254),
                new Column("PARTITION_ID", DbType.Decimal),
                new Column("OTHER", DbType.Decimal));

            this.Database.CreateSequence("SEQ_PLAN_TABLE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PLAN_TABLE");
            this.Database.RemoveSequence("SEQ_PLAN_TABLE");
        }
    }
}
