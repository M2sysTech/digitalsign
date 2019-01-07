namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201607081638)]
    public class CriaComparaBio : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ComparaBio",
                this.WithId("Compara_Code"),                     
                new Column("Face_Code1", DbType.Int32),
                new Column("Face_Code2", DbType.Int32),
                new Column("Compara_Percent", DbType.Double),
                new Column("Compara_Status", DbType.AnsiString, 1),
                new Column("Compara_Result", DbType.AnsiString, 2),
                new Column("Compara_StartTime", DbType.DateTime),
                new Column("Usr_Code", DbType.Int32));

            this.Database.CreateSequence("seq_ComparaBio");

            this.Database.AddTable(
                "ComparaBio_Bk",
                this.WithId("Compara_Code"),
                new Column("Face_Code1", DbType.Int32),
                new Column("Face_Code2", DbType.Int32),
                new Column("Compara_Percent", DbType.Double),
                new Column("Compara_Status", DbType.AnsiString, 1),
                new Column("Compara_Result", DbType.AnsiString, 2),
                new Column("Compara_StartTime", DbType.DateTime),
                new Column("Usr_Code", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveTable("ComparaBio");
            this.Database.RemoveSequence("seq_ComparaBio");
            this.Database.RemoveTable("ComparaBio_Bk");
        }
    }
}
