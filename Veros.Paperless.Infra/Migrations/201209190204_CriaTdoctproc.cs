namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190204)]
    public class CriaTdoctproc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TDOCTPROC",
                this.WithId("TDOCTPROC_CODE"),
                new Column("TYPEDOC_ID", DbType.Int32, ColumnProperty.NotNull),
                new Column("TYPEPROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("TDOCTPROC_QTDPAG", DbType.Int32),
                new Column("TDOCTPROC_OBRIGATORIO", DbType.AnsiString, 1),
                new Column("TDOCTPROC_CODEXPORT", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_TDOCTPROC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TDOCTPROC");
            this.Database.RemoveSequence("SEQ_TDOCTPROC");
        }
    }
}
