namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;
    
    [Migration(201303281500)]
    public class AlteraColunaVinculoEmRegraproc : Migration
    {
        public override void Up()
        {
            if (Veros.Data.Database.ProviderName.Equals("Oracle"))
            {
                Database.ExecuteNonQuery("ALTER TABLE regraproc ADD regraimp_vinculo_temp VARCHAR2(10)");
                Database.ExecuteNonQuery("UPDATE regraproc SET    regraimp_vinculo_temp =  regraimp_vinculo");
                Database.ExecuteNonQuery("ALTER TABLE regraproc DROP COLUMN     regraimp_vinculo");
                Database.ExecuteNonQuery("ALTER TABLE regraproc RENAME COLUMN  regraimp_vinculo_temp TO regraimp_vinculo");
            }
            else
            {
                this.Database.ChangeColumn(
                    "REGRAPROC",
                    new Column("REGRAIMP_VINCULO", DbType.AnsiString, 10));
            }
        }

        public override void Down()
        {
        }
    }
}