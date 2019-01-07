namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190018)]
    public class CriaBackups : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "BACKUPS",
                this.WithId("BACKUPS_CODE"),
                new Column("BKPTYPE_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("BACKUP_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("BACKUP_RECDATE", DbType.DateTime),
                new Column("BACKUP_VALDATE", DbType.DateTime),
                new Column("BACKUP_OBS", DbType.AnsiString, 4000),
                new Column("BACKUP_USR", DbType.Int32, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_BACKUPS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("BACKUPS");
            this.Database.RemoveSequence("SEQ_BACKUPS");
        }
    }
}
