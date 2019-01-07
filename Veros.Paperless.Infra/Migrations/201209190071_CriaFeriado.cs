namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190071)]
    public class CriaFeriado : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "FERIADO",
                this.WithId("FERIADO_CODE"),
                new Column("FERIADO_DATA", DbType.DateTime, ColumnProperty.NotNull),
                new Column("FERIADO_ST", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_FERIADO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("FERIADO");
            this.Database.RemoveSequence("SEQ_FERIADO");
        }
    }
}
