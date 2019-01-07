namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190205)]
    public class CriaTdoctprocgus : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TDOCTPROCGUS",
                this.WithId("TDOCTPROCGUS_CODE"),
                new Column("TYPEDOC_ID", DbType.Int32, ColumnProperty.NotNull),
                new Column("TYPEPROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("TDOCTPROC_QTDPAG", DbType.Int32),
                new Column("TDOCTPROC_OBRIGATORIO", DbType.AnsiString, 1),
                new Column("TDOCTPROC_CODEXPORT", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_TDOCTPROCGUS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TDOCTPROCGUS");
            this.Database.RemoveSequence("SEQ_TDOCTPROCGUS");
        }
    }
}
