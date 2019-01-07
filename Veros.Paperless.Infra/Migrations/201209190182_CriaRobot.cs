namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190182)]
    public class CriaRobot : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ROBOT",
                this.WithId("ROBOT_CODE"),
                new Column("AGENCIA_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("ROBOT_HI", DbType.AnsiString, 5, ColumnProperty.NotNull),
                new Column("ROBOT_HF", DbType.AnsiString, 5, ColumnProperty.NotNull),
                new Column("ROBOT_TXMAX", DbType.Int32));

            this.Database.CreateSequence("SEQ_ROBOT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ROBOT");
            this.Database.RemoveSequence("SEQ_ROBOT");
        }
    }
}
