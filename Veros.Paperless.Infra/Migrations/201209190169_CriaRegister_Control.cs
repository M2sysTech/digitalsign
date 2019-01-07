namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190169)]
    public class CriaRegisterControl : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "REGISTER_CONTROL",
                this.WithId("REGISTER_CONTROL_CODE"),
                new Column("REGISTER_CODE", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("REGISTERCONTROL_LASTACC", DbType.DateTime, ColumnProperty.NotNull),
                new Column("REGISTERCONTROL_USER", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("REGISTERCONTROL_VERSION", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_REGISTER_CONTROL");
        }

        public override void Down()
        {
            this.Database.RemoveTable("REGISTER_CONTROL");
            this.Database.RemoveSequence("SEQ_REGISTER_CONTROL");
        }
    }
}
