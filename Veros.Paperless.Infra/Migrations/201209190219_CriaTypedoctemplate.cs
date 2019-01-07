namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190219)]
    public class CriaTypedoctemplate : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TYPEDOCTEMPLATE",
                this.WithId("TEMPLATE_CODE"),
                new Column("TEMPLATE_NAME", DbType.AnsiString, 50, ColumnProperty.NotNull),
                new Column("TEMPLATE_KEYS", DbType.AnsiString, 250),
                new Column("TEMPLATE_ORDER", DbType.Int32),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("TEMPLATE_EXTENSAO", DbType.AnsiString, 4));

            this.Database.CreateSequence("SEQ_TYPEDOCTEMPLATE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TYPEDOCTEMPLATE");
            this.Database.RemoveSequence("SEQ_TYPEDOCTEMPLATE");
        }
    }
}
