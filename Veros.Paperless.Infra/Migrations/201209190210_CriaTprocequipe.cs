namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190210)]
    public class CriaTprocequipe : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TPROCEQUIPE",
                this.WithId("TPROCEQUIPE_CODE"),
                new Column("TYPEPROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("TPROCEQUIPE_PRIORIDADE", DbType.Int32));

            this.Database.CreateSequence("SEQ_TPROCEQUIPE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TPROCEQUIPE");
            this.Database.RemoveSequence("SEQ_TPROCEQUIPE");
        }
    }
}
