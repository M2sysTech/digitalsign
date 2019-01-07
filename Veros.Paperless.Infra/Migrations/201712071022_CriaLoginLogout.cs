namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201712071022)]
    public class CriaLoginLogout : Migration
    {
        public override void Up()
        {
            this.Database.AddTable("LOGINLOGOUT", 
                this.WithId("LOGINLOGOUT_CODE"), 
                new Column("USR_CODE", DbType.Int32), 
                new Column("LOGINLOGOUT_DTIN", DbType.Date), 
                new Column("LOGINLOGOUT_DTOUT", DbType.Date), 
                new Column("LOGINLOGOUT_MAQUINA", DbType.AnsiString, 255));

            this.Database.CreateSequence("SEQ_LOGINLOGOUT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGINLOGOUT");
            this.Database.RemoveSequence("SEQ_LOGINLOGOUT");
        }
    }
}
