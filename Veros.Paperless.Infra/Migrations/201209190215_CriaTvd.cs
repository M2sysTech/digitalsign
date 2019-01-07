namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190215)]
    public class CriaTvd : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TVD",
                this.WithId("TVD_CODE"),
                new Column("TVD_DOC01", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR01", DbType.AnsiString, 25),
                new Column("TVD_DOC02", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR02", DbType.AnsiString, 25),
                new Column("TVD_DOC03", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR03", DbType.AnsiString, 25),
                new Column("TVD_DOC04", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR04", DbType.AnsiString, 25),
                new Column("TVD_DOC05", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR05", DbType.AnsiString, 25),
                new Column("TVD_DOC06", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR06", DbType.AnsiString, 25),
                new Column("TVD_DOC07", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR07", DbType.AnsiString, 25),
                new Column("TVD_DOC08", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR08", DbType.AnsiString, 25),
                new Column("TVD_DOC09", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR09", DbType.AnsiString, 25),
                new Column("TVD_DOC10", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR10", DbType.AnsiString, 25),
                new Column("TVD_DOC11", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR11", DbType.AnsiString, 25),
                new Column("TVD_DOC12", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR12", DbType.AnsiString, 25),
                new Column("TVD_DOC13", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR13", DbType.AnsiString, 25),
                new Column("TVD_DOC14", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR14", DbType.AnsiString, 25),
                new Column("TVD_DOC15", DbType.AnsiString, 6),
                new Column("TVD_DEVEDOR15", DbType.AnsiString, 25));

            this.Database.CreateSequence("SEQ_TVD");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TVD");
            this.Database.RemoveSequence("SEQ_TVD");
        }
    }
}
