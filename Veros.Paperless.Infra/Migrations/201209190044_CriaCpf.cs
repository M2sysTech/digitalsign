namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190044)]
    public class CriaCpf : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CPF",
                this.WithId("CPF_CODE"),
                new Column("CPF_BCOAGCTA", DbType.AnsiString, 16, ColumnProperty.NotNull),
                new Column("CPF_NUM", DbType.AnsiString, 16),
                new Column("CPF_DT", DbType.DateTime));

            this.Database.CreateSequence("SEQ_CPF");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CPF");
            this.Database.RemoveSequence("SEQ_CPF");
        }
    }
}
