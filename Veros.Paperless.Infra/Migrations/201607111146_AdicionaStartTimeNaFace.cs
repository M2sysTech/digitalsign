namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201607111146)]
    public class AdicionaStartTimeNaFace : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("FACE", new Column("FACE_STARTTIME", DbType.DateTime));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("FACE", "FACE_STARTTIME");
        }
    }
}
