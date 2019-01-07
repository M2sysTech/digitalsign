namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190122)]
    public class CriaMdocLixo : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MDOC_LIXO",
                this.WithId("MDOC_CODE"),
                new Column("TYPEDOC_ID", DbType.Int32, ColumnProperty.NotNull),
                new Column("MDOC_STATUS", DbType.AnsiString, 2, ColumnProperty.NotNull),
                new Column("BATCH_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("PROC_CODE", DbType.Int32),
                new Column("MDOC_DADOS", DbType.AnsiString, 4000),
                new Column("MDOC_USRRESP", DbType.Int32),
                new Column("MDOC_OCORRENCIA", DbType.AnsiString, 2),
                new Column("MDOC_STARTTIME", DbType.DateTime),
                new Column("MDOC_USRDIGIT", DbType.Int32),
                new Column("MDOC_MARCA", DbType.AnsiString, 1),
                new Column("USR_DIGIT", DbType.Int32),
                new Column("USR_VALID", DbType.Int32));

            this.Database.CreateSequence("SEQ_MDOC_LIXO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MDOC_LIXO");
            this.Database.RemoveSequence("SEQ_MDOC_LIXO");
        }
    }
}
