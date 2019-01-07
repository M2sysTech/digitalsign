namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190037)]
    public class CriaCompany : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "COMPANY",
                this.WithId("COMPANY_CODE"),
                new Column("COMPANY_NAME", DbType.AnsiString, 254, ColumnProperty.NotNull),
                new Column("COMPANY_ALIAS", DbType.AnsiString, 16, ColumnProperty.NotNull),
                new Column("COMPANY_ADDRESS", DbType.AnsiString, 127),
                new Column("COMPANY_CITY", DbType.AnsiString, 127),
                new Column("COMPANY_ZIP", DbType.AnsiString, 16),
                new Column("COMPANY_PHONE", DbType.AnsiString, 127),
                new Column("COMPANY_FAX", DbType.AnsiString, 127),
                new Column("COMPANY_EMAIL", DbType.AnsiString, 127),
                new Column("COMPANY_LOGO", DbType.AnsiString, 254));

            this.Database.CreateSequence("SEQ_COMPANY");
        }

        public override void Down()
        {
            this.Database.RemoveTable("COMPANY");
            this.Database.RemoveSequence("SEQ_COMPANY");
        }
    }
}
