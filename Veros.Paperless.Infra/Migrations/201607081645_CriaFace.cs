namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201607081645)]
    public class CriaFace : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "FACE",
                this.WithId("FACE_CODE"),
                new Column("DOC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("FACE_FILETYPE", DbType.AnsiString, 4),
                new Column("FACE_ARQUIVO", DbType.AnsiString, 64),
                new Column("FACE_STATUSCOMP", DbType.AnsiString, 1),
                new Column("FACE_CPF", DbType.AnsiString, 14),
                new Column("FACE_COMUM", DbType.AnsiString, 1),
                new Column("FACE_BLACKLIST", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_FACE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("FACE");
            this.Database.RemoveSequence("SEQ_FACE");
        }
    }
}
