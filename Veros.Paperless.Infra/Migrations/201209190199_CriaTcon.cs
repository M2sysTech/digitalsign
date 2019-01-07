namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190199)]
    public class CriaTcon : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TCON",
                this.WithId("TCON_CODE"),
                new Column("TCON_CONVENIO", DbType.AnsiString, 6),
                new Column("TCON_EMPRESA", DbType.AnsiString, 25),
                new Column("TCON_SISTEMA", DbType.AnsiString, 1),
                new Column("TCON_ACEITACH", DbType.AnsiString, 1),
                new Column("TCON_TIPOCAPT", DbType.AnsiString, 2),
                new Column("TCON_ATRASO", DbType.AnsiString, 1),
                new Column("TCON_POSTANT", DbType.AnsiString, 1),
                new Column("TCON_POSINIDTV", DbType.Int32),
                new Column("TCON_TAMANHODTV", DbType.Int32),
                new Column("TCON_FORMATODTV", DbType.AnsiString, 3),
                new Column("TCON_CNPJ", DbType.AnsiString, 8),
                new Column("TCON_ST", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_TCON");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TCON");
            this.Database.RemoveSequence("SEQ_TCON");
        }
    }
}
