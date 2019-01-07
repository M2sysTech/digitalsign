namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201305241630)]
    public class CriaTarja : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TARJA",
                this.WithId("TARJA_CODE"),
                new Column("TDCAMPOS_CODE", DbType.Int32 ),
                new Column("MDOC_CODE", DbType.Int32 ),
                new Column("DOC_CODE", DbType.Int32),
                new Column("TARJA_QTDRETANGULOS", DbType.Int16),
                new Column("TARJA_POSICOES", DbType.String, 300));

            this.Database.CreateSequence("SEQ_TARJA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TARJA");
            this.Database.RemoveSequence("SEQ_TARJA");
        }
    }
}
