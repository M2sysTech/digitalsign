namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201305131353)]
    public class CriaExpurgoConfig : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "EXPURGOCONFIG",
                this.WithId("EXPURGOCONFIG_CODE"),
                new Column("EXPURGOCONFIG_INTERVALODIAS", DbType.Int32),
                new Column("EXPURGOCONFIG_HORARIO", DbType.Int32),
                new Column("EXPURGOCONFIG_APAGARIMAGENS", DbType.Boolean),
                new Column("EXPURGOCONFIG_ULTIMOEXPURGO", DbType.DateTime));

            var sql = @"ALTER TABLE EXPURGOCONFIG MODIFY (
                                 EXPURGOCONFIG_ULTIMOEXPURGO DATE
                       )";

            this.Database.ExecuteNonQuery(sql);

            this.Database.CreateSequence("SEQ_EXPURGOCONFIG");
        }

        public override void Down()
        {
            this.Database.RemoveTable("EXPURGOCONFIG");
            this.Database.RemoveSequence("SEQ_EXPURGOCONFIG");
        }
    }
}
