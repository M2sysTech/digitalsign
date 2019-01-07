namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201806141500)]
    public class CriaLogPack : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGPACK",
                this.WithId("LOGPACK_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("PACK_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("LOGPACK_DATETIME", DbType.Date, ColumnProperty.NotNull),
                new Column("LOGPACK_ACTION", DbType.AnsiString, 3, ColumnProperty.NotNull),                
                new Column("LOGPACK_OBS", DbType.AnsiString, 254));

            this.Database.CreateSequence("SEQ_LOGPACK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGPACK");
            this.Database.RemoveSequence("SEQ_LOGPACK");
        }
    }
}
