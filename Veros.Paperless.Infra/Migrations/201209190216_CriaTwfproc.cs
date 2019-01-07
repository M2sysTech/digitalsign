namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190216)]
    public class CriaTwfproc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TWFPROC",
                this.WithId("TWFPROC_CODE"),
                new Column("TYPEPROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("TWFPROC_OBS", DbType.AnsiString, 127),
                new Column("EQUIPE_DESC", DbType.AnsiString, 127),
                new Column("EQUIPE_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_TWFPROC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TWFPROC");
            this.Database.RemoveSequence("SEQ_TWFPROC");
        }
    }
}
