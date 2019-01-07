namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190115)]
    public class CriaMdoc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MDOC",
                this.WithId("MDOC_CODE"),
                new Column("TYPEDOC_ID", DbType.Int32, ColumnProperty.NotNull),
                new Column("MDOC_STATUS", DbType.AnsiString, 2, ColumnProperty.NotNull),
                new Column("BATCH_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("PROC_CODE", DbType.Int32),
                new Column("MDOC_USRRESP", DbType.Int32),
                new Column("MDOC_OCORRENCIA", DbType.AnsiString, 2),
                new Column("MDOC_STARTTIME", DbType.DateTime),
                new Column("MDOC_USRDIGIT", DbType.Int32),
                new Column("MDOC_MARCA", DbType.AnsiString, 1),
                new Column("USR_DIGIT", DbType.Int32),
                new Column("USR_VALID", DbType.Int32),
                new Column("MDOC_DADOS", DbType.AnsiString, 4000),
                new Column("TYPEPROC_ID", DbType.AnsiString, 5),
                new Column("SEGMENTO_ID", DbType.AnsiString, 5),
                new Column("MDOC_TEMPLATES", DbType.AnsiString, 100));

            this.Database.CreateSequence("SEQ_MDOC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MDOC");
            this.Database.RemoveSequence("SEQ_MDOC");
        }
    }
}
