namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190007)]
    public class CriaAcesso : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ACESSO",
                this.WithId("ACESSO_CODE"),
                new Column("PERFIL_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("ACESSO_REGRANUM", DbType.Int32, ColumnProperty.NotNull),
                new Column("ACESSO_STATUS", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_ACESSO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ACESSO");
            this.Database.RemoveSequence("SEQ_ACESSO");
        }
    }
}
