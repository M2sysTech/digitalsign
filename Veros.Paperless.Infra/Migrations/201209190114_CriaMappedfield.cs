namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190114)]
    public class CriaMappedfield : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MAPPEDFIELD",
                this.WithId("MAPPEDFIELD_CODE"),
                new Column("MAPPEDFIELD_ABBYYTEMPLATENAME", DbType.AnsiString, 50, ColumnProperty.NotNull),
                new Column("TDCAMPOS_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("MAPPEDFIELD_ABBYYFIELDNAME", DbType.AnsiString, 50, ColumnProperty.NotNull),
                new Column("MAPPEDFIELD_SEARCHBLOCK", DbType.Int32),
                new Column("MAPPEDFIELD_DATATYPE", DbType.Int32));

            this.Database.CreateSequence("SEQ_MAPPEDFIELD");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MAPPEDFIELD");
            this.Database.RemoveSequence("SEQ_MAPPEDFIELD");
        }
    }
}
