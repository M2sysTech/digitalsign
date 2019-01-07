namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201310181146)]
    public class InsereQueueOcrNoIpConfig : Migration
    {
        public override void Up()
        {
            this.Database.ExecuteNonQuery(@"
insert into 
    IPCONFIG 
(IPCONFIG_CODE, IPCONFIG_DTCENTERID, IPCONFIG_TAG, IPCONFIG_IP, IPCONFIG_PORT)
    values
(SEQ_IPCONFIG.nextval, '1', 'QUEUEOCR', '127.0.0.1', '7778')");
        }

        public override void Down()
        {
        }
    }
}