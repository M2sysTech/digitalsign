namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190129)]
    public class CriaMunic : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MUNIC",
                this.WithId("MUNIC_CODE"),
                new Column("MUNIC_NUM", DbType.AnsiString, 6, ColumnProperty.NotNull),
                new Column("MUNIC_NOME", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("MUNIC_UF", DbType.AnsiString, 2),
                new Column("MUNIC_CAPITAL", DbType.AnsiString, 1),
                new Column("MUNIC_ACESSO", DbType.AnsiString, 1),
                new Column("MUNIC_ST", DbType.AnsiString, 1),
                new Column("MUNIC_X", DbType.Int32),
                new Column("MUNIC_Y", DbType.Int32));

            this.Database.CreateSequence("SEQ_MUNIC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MUNIC");
            this.Database.RemoveSequence("SEQ_MUNIC");
        }
    }
}
