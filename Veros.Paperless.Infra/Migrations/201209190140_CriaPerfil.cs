namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190140)]
    public class CriaPerfil : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PERFIL",
                this.WithId("PERFIL_CODE"),
                new Column("PERFIL_SIGLA", DbType.AnsiString, 5, ColumnProperty.NotNull),
                new Column("PERFIL_DESC", DbType.AnsiString, 30, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_PERFIL");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PERFIL");
            this.Database.RemoveSequence("SEQ_PERFIL");
        }
    }
}
