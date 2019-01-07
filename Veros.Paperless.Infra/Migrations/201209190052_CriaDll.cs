namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190052)]
    public class CriaDll : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DLL",
                this.WithId("DLL_CODE"),
                new Column("DLL_NAME", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("DLL_VERSION", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("DLL_RESTART", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_DLL");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DLL");
            this.Database.RemoveSequence("SEQ_DLL");
        }
    }
}
