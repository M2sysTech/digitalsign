namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190164)]
    public class CriaRds : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "RDS",
                this.WithId("RDS_CODE"),
                new Column("RDS_WIDTH", DbType.Int32),
                new Column("RDS_HEIGHT", DbType.Int32),
                new Column("RDS_OFF_F", DbType.Int32),
                new Column("RDS_OFF_V", DbType.Int32),
                new Column("RDS_BRI_F", DbType.Int32),
                new Column("RDS_BRI_V", DbType.Int32),
                new Column("RDS_CONT_F", DbType.Int32),
                new Column("RDS_CONT_V", DbType.Int32),
                new Column("RDS_CUTTODOC", DbType.AnsiString, 1),
                new Column("RDS_RES_FG", DbType.Int32),
                new Column("RDS_TIPO_FG", DbType.Int32),
                new Column("RDS_COMP_FG", DbType.Int32),
                new Column("RDS_RES_FR", DbType.Int32),
                new Column("RDS_TIPO_FR", DbType.Int32),
                new Column("RDS_COMP_FR", DbType.Int32),
                new Column("RDS_CORV", DbType.Int32),
                new Column("RDS_RESV", DbType.Int32),
                new Column("RDS_TIPOV", DbType.Int32),
                new Column("RDS_COMPV", DbType.Int32));

            this.Database.CreateSequence("SEQ_RDS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("RDS");
            this.Database.RemoveSequence("SEQ_RDS");
        }
    }
}
