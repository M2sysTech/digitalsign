namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190213)]
    public class CriaTratdig : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TRATDIG",
                this.WithId("TRATDIG_CODE"),
                new Column("TRATDIG_ENABLED", DbType.AnsiString, 1),
                new Column("TRATDIG_CLIENT", DbType.AnsiString, 1),
                new Column("TRATDIG_CROP", DbType.AnsiString, 1),
                new Column("TRATDIG_DESKEW", DbType.AnsiString, 1),
                new Column("TRATDIG_DESPECK", DbType.AnsiString, 1),
                new Column("TRATDIG_BINARIZ", DbType.AnsiString, 1),
                new Column("TRATDIG_PALLETE", DbType.Int32),
                new Column("TRATDIG_DITHER", DbType.Int32),
                new Column("TRATDIG_DESPLOOP", DbType.Int32),
                new Column("TRATDIG_LUMFACT", DbType.Int32),
                new Column("TRATDIG_CROMFACT", DbType.Int32),
                new Column("TRATDIG_BRILHO", DbType.Int32),
                new Column("TRATDIG_CONTRAST", DbType.Int32),
                new Column("TRATDIG_BLEND", DbType.Int32),
                new Column("TRATDIG_LUMINAN", DbType.Int32),
                new Column("TRATDIG_ICHECK", DbType.AnsiString, 1),
                new Column("TRATDIG_LOGOERAS", DbType.AnsiString, 1),
                new Column("TRATDIG_BINREFIN", DbType.AnsiString, 1),
                new Column("TRATDIG_DTBOA", DbType.AnsiString, 1),
                new Column("TRATDIG_DBID", DbType.AnsiString, 3));

            this.Database.CreateSequence("SEQ_TRATDIG");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TRATDIG");
            this.Database.RemoveSequence("SEQ_TRATDIG");
        }
    }
}
