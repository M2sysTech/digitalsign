namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190206)]
    public class CriaTemplatecampos : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TEMPLATECAMPOS",
                new Column("TEMPLATE_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("TDCAMPOS_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("POSICAO", DbType.AnsiString, 127),
                new Column("TEMPLATECAMPOS_PAGINA", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_TEMPLATECAMPOS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TEMPLATECAMPOS");
            this.Database.RemoveSequence("SEQ_TEMPLATECAMPOS");
        }
    }
}
