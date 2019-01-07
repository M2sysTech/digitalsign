namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190190)]
    public class CriaSimul : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SIMUL",
                this.WithId("SIMUL_CODE"),
                new Column("SIMUL_DELAYENV", DbType.Int32),
                new Column("SIMUL_DELAYDEP", DbType.Int32),
                new Column("SIMUL_DELAYCHQ", DbType.Int32),
                new Column("SIMUL_VALDEP", DbType.Decimal),
                new Column("SIMUL_VALCHQ", DbType.Decimal));

            this.Database.CreateSequence("SEQ_SIMUL");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SIMUL");
            this.Database.RemoveSequence("SEQ_SIMUL");
        }
    }
}
