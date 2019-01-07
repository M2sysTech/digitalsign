namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190180)]
    public class CriaRmeBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "RME_BK",
                this.WithId("RME_CODE"),
                new Column("DOC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("RME_01AGENC", DbType.AnsiString, 127),
                new Column("RME_01FONE", DbType.AnsiString, 127),
                new Column("RME_01BDU", DbType.AnsiString, 127),
                new Column("RME_01REDE", DbType.AnsiString, 127),
                new Column("RME_01DATA", DbType.DateTime),
                new Column("RME_01VALOR", DbType.Decimal),
                new Column("RME_01HORARIO", DbType.AnsiString, 127),
                new Column("RME_02TIPO", DbType.AnsiString, 1),
                new Column("RME_03TIPO", DbType.AnsiString, 1),
                new Column("RME_03NUM", DbType.AnsiString, 127),
                new Column("RME_03TIT", DbType.AnsiString, 127),
                new Column("RME_03CPF", DbType.AnsiString, 127),
                new Column("RME_04NOME", DbType.AnsiString, 127),
                new Column("RME_04CPF", DbType.AnsiString, 127),
                new Column("RME_05NOME", DbType.AnsiString, 127),
                new Column("RME_05CPF", DbType.AnsiString, 127),
                new Column("RME_06NOME", DbType.AnsiString, 127),
                new Column("RME_06CPF", DbType.AnsiString, 127),
                new Column("RME_07NOME1", DbType.AnsiString, 127),
                new Column("RME_07CPF1", DbType.AnsiString, 127),
                new Column("RME_07BANCO1", DbType.AnsiString, 127),
                new Column("RME_07AGENC1", DbType.AnsiString, 127),
                new Column("RME_07CONTA1", DbType.AnsiString, 127),
                new Column("RME_07NOME2", DbType.AnsiString, 127),
                new Column("RME_07CPF2", DbType.AnsiString, 127),
                new Column("RME_07BANCO2", DbType.AnsiString, 127),
                new Column("RME_07AGENC2", DbType.AnsiString, 127),
                new Column("RME_07CONTA2", DbType.AnsiString, 127),
                new Column("RME_08NOME", DbType.AnsiString, 127),
                new Column("RME_08CPF", DbType.AnsiString, 127),
                new Column("RME_09NOME", DbType.AnsiString, 127),
                new Column("RME_09CPF", DbType.AnsiString, 127),
                new Column("RME_10GER1", DbType.AnsiString, 127),
                new Column("RME_10MAT1", DbType.AnsiString, 127),
                new Column("RME_10GER2", DbType.AnsiString, 127),
                new Column("RME_10MAT2", DbType.AnsiString, 127),
                new Column("RME_11OBS", DbType.AnsiString, 254));

            this.Database.CreateSequence("SEQ_RME_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("RME_BK");
            this.Database.RemoveSequence("SEQ_RME_BK");
        }
    }
}
