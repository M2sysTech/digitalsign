namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190234)]
    public class CriaVEndereco : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "V_ENDERECO",
                this.WithId("V_CODE"),
                new Column("DS_LOGRADOURO_NOME", DbType.AnsiString, 80, ColumnProperty.NotNull),
                new Column("DS_BAIRRO_NOME", DbType.AnsiString, 80),
                new Column("DS_CIDADE_NOME", DbType.AnsiString, 80, ColumnProperty.NotNull),
                new Column("DS_UF_NOME", DbType.AnsiString, 80),
                new Column("DS_UF_SIGLA", DbType.AnsiString, 20));

            this.Database.CreateSequence("SEQ_V_ENDERECO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("V_ENDERECO");
            this.Database.RemoveSequence("SEQ_V_ENDERECO");
        }
    }
}
