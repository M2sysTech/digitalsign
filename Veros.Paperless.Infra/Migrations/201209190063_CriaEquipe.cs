namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190063)]
    public class CriaEquipe : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "EQUIPE",
                this.WithId("EQUIPE_CODE"),
                new Column("EQUIPE_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("USR_GERENTE", DbType.Int32));

            this.Database.CreateSequence("SEQ_EQUIPE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("EQUIPE");
            this.Database.RemoveSequence("SEQ_EQUIPE");
        }
    }
}
