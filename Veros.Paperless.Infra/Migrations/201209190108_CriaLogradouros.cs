namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190108)]
    public class CriaLogradouros : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGRADOUROS",
                this.WithId("LOGRADOUROS_CODE"),
                new Column("CD_BAIRRO", DbType.Int32, ColumnProperty.NotNull),
                new Column("CD_TIPO_LOGRADOUROS", DbType.Int32, ColumnProperty.NotNull),
                new Column("DS_LOGRADOURO_NOME", DbType.AnsiString, 80, ColumnProperty.NotNull),
                new Column("NO_LOGRADOURO_CEP", DbType.AnsiString, 8));

            this.Database.CreateSequence("SEQ_LOGRADOUROS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGRADOUROS");
            this.Database.RemoveSequence("SEQ_LOGRADOUROS");
        }
    }
}
