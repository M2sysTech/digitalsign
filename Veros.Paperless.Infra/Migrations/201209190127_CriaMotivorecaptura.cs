namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190127)]
    public class CriaMotivorecaptura : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MOTIVORECAPTURA",
                this.WithId("MOTIVORECAPTURA_CODE"),
                new Column("MOTIVORECAPTURA_DESC", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_MOTIVORECAPTURA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MOTIVORECAPTURA");
            this.Database.RemoveSequence("SEQ_MOTIVORECAPTURA");
        }
    }
}
