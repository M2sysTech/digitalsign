namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190064)]
    public class CriaEquipeproc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "EQUIPEPROC",
                this.WithId("EQUIPEPROC_CODE"),
                new Column("PROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("USR_CODE", DbType.Int32),
                new Column("EQUIPEPROC_STARTTIME", DbType.DateTime));

            this.Database.CreateSequence("SEQ_EQUIPEPROC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("EQUIPEPROC");
            this.Database.RemoveSequence("SEQ_EQUIPEPROC");
        }
    }
}
