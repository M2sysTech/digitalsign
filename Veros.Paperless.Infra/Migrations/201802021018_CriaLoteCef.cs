namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201802021018)]
    public class CriaLoteCef : Migration
    {
        public override void Up()
        {
            this.Database.AddTable("LOTECEF",
                this.WithId("LOTECEF_CODE"),
                new Column("LOTECEF_DTCRIACAO", DbType.Date),
                new Column("LOTECEF_DTFIM", DbType.Date),
                new Column("LOTECEF_STATUS", DbType.AnsiString, 3),
                new Column("LOTECEF_VISIVEL", DbType.Int32),
                new Column("LOTECEF_QTBATCH", DbType.Int32),
                new Column("LOTECEF_DTAPROV", DbType.Date),
                new Column("USR_APROV", DbType.Int32));

            this.Database.CreateSequence("SEQ_LOTECEF");

            this.Database.AddColumn("BATCH", new Column("LOTECEF_CODE", DbType.Int32));

            this.Database.AddTable("CONFIGLOTECEF",
                this.WithId("CONFIGLC_CODE"),
                new Column("CONFIGLC_HORAFECHA", DbType.Date),
                new Column("CONFIGLC_QTMIN", DbType.Int32),
                new Column("CONFIGLC_QTMAX", DbType.Int32));

            this.Database.CreateSequence("SEQ_CONFIGLOTECEF");

            this.Database.ExecuteNonQuery(@"
INSERT INTO CONFIGLOTECEF (
  CONFIGLC_CODE, CONFIGLC_HORAFECHA, CONFIGLC_QTMIN, CONFIGLC_QTMAX
) VALUES (
  SEQ_CONFIGLOTECEF.NEXTVAL,
  To_Date('01/01/2000 14:00', 'DD/MM/YYYY HH24:MI'),
  3,
  10
)");
        }

        public override void Down()
        {
        }
    }
}
