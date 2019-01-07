namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190168)]
    public class CriaRegister : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "REGISTER",
                new Column("REGISTER_CODE", DbType.AnsiString, 4000),
                new Column("REGISTER_STATION", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("REGISTER_DATE", DbType.DateTime, ColumnProperty.NotNull),
                new Column("REGISTER_VERSION", DbType.AnsiString, 127),
                new Column("REGISTER_LASTACC", DbType.DateTime),
                new Column("REGISTER_UPDATE", DbType.AnsiString, 1),
                new Column("REGISTER_OBS", DbType.AnsiString, 4000),
                new Column("REGISTER_LASTIMG", DbType.Int32),
                new Column("REGISTER_MAQLOG", DbType.AnsiString, 1),
                new Column("REGISTER_VER2", DbType.AnsiString, 127),
                new Column("REGISTER_USER", DbType.AnsiString, 127),
                new Column("REGISTER_BDU", DbType.AnsiString, 16),
                new Column("REGISTER_NUMBER", DbType.AnsiString, 127),
                new Column("REGISTER_DBID", DbType.AnsiString, 127),
                new Column("AMBIENT_CODE", DbType.Int32),
                new Column("AGENCIA_NUM", DbType.AnsiString, 127),
                new Column("REGISTER_USERAUTORIZ", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_REGISTER");
        }

        public override void Down()
        {
            this.Database.RemoveTable("REGISTER");
            this.Database.RemoveSequence("SEQ_REGISTER");
        }
    }
}
