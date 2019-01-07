namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190031)]
    public class CriaCcBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CC_BK",
                this.WithId("CC_CODE"),
                new Column("CC_AGENCIA", DbType.AnsiString, 4, ColumnProperty.NotNull),
                new Column("CC_OPERACAO", DbType.AnsiString, 1, ColumnProperty.NotNull),
                new Column("CC_CONTA", DbType.AnsiString, 12, ColumnProperty.NotNull),
                new Column("CC_NOMETIT1", DbType.AnsiString, 127),
                new Column("CC_NOMETIT2", DbType.AnsiString, 127),
                new Column("CC_NOMETIT3", DbType.AnsiString, 127),
                new Column("CC_NOMETIT4", DbType.AnsiString, 127),
                new Column("CC_CNPJ", DbType.AnsiString, 25),
                new Column("CC_TIPOCNPJ", DbType.AnsiString, 1),
                new Column("CC_ENDERECO", DbType.AnsiString, 127),
                new Column("CC_NUMERO", DbType.AnsiString, 6),
                new Column("CC_COMPLEM", DbType.AnsiString, 10),
                new Column("CC_BAIRRO", DbType.AnsiString, 127),
                new Column("CC_CIDADE", DbType.AnsiString, 127),
                new Column("CC_UF", DbType.AnsiString, 2),
                new Column("CC_CEP", DbType.AnsiString, 10),
                new Column("CC_TELEFONE", DbType.AnsiString, 8),
                new Column("CC_MARCAS", DbType.AnsiString, 150));

            this.Database.CreateSequence("SEQ_CC_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CC_BK");
            this.Database.RemoveSequence("SEQ_CC_BK");
        }
    }
}
