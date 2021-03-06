namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190067)]
    public class CriaExportconfiggus : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "EXPORTCONFIGGUS",
                this.WithId("EXPORTCONFIGGUS_CODE"),
                new Column("TYPEPROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("EXPORTCONFIG_FILENAME", DbType.AnsiString, 150),
                new Column("EXPORTCONFIG_CABECALHO", DbType.AnsiString, 1),
                new Column("EXPORTCONFIG_CABECALHOTEXTO", DbType.AnsiString, 1024),
                new Column("EXPORTCONFIG_RODAPE", DbType.AnsiString, 1),
                new Column("EXPORTCONFIG_RODAPETEXTO", DbType.AnsiString, 1024),
                new Column("EXPORTCONFIG_SEPARADORCOLUNA", DbType.AnsiString, 15));

            this.Database.CreateSequence("SEQ_EXPORTCONFIGGUS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("EXPORTCONFIGGUS");
            this.Database.RemoveSequence("SEQ_EXPORTCONFIGGUS");
        }
    }
}
