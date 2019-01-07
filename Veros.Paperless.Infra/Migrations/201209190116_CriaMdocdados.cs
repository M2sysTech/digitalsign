namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190116)]
    public class CriaMdocdados : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MDOCDADOS",
                this.WithId("MDOCDADOS_CODE"),
                new Column("MDOC_CODE", DbType.Int32, ColumnProperty.NotNull),
                ////new Column("MDOCDADOS_VALOR1", DbType.StringFixedLength, 4000),
                ////new Column("MDOCDADOS_VALOR2", DbType.StringFixedLength, 4000),
                ////new Column("MDOCDADOS_VALOROK", DbType.StringFixedLength, 4000),
                new Column("MDOCDADOS_STATUSFORM", DbType.AnsiString, 3),
                new Column("TDCAMPOS_CODE", DbType.Int32),
                new Column("MDOCDADOS_STARTTIME1", DbType.DateTime),
                new Column("MDOCDADOS_STARTTIME2", DbType.DateTime),
                new Column("MDOCDADOS_USRDIGIT1", DbType.Int32),
                new Column("MDOCDADOS_USRDIGIT2", DbType.Int32),
                new Column("MDOCDADOS_FULLVIEW", DbType.Int32));

            this.Database.CreateSequence("SEQ_MDOCDADOS");

            //// TODO: um jeito mais simples de criar esse campo VARCHAR2(4000). Migrations está criando CLOB
            string sql = @"
alter table MDOCDADOS add (
    mdocdados_valor1     VARCHAR2(4000) NULL,
    mdocdados_valor2     VARCHAR2(4000) NULL,
    mdocdados_valorok    VARCHAR2(4000) NULL)";

            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            this.Database.RemoveTable("MDOCDADOS");
            this.Database.RemoveSequence("SEQ_MDOCDADOS");
        }
    }
}
