namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190051)]
    public class CriaDiapico : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DIAPICO",
                this.WithId("DIAPICO_CODE"),
                new Column("DIAPICO_DATE", DbType.DateTime, ColumnProperty.NotNull),
                new Column("DIAPICO_FATOR", DbType.Decimal),
                new Column("DIAPICO_VOLUME", DbType.Int32),
                new Column("DIAPICO_DTREF", DbType.DateTime));

            this.Database.CreateSequence("SEQ_DIAPICO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DIAPICO");
            this.Database.RemoveSequence("SEQ_DIAPICO");
        }
    }
}
