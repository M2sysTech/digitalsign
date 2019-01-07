namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201211061146)]
    public class CriaCamposValicadao : Migration
    {
        public override void Up()
        {
            this.Database.AddTable("CAMPOVALIDACAO", 
                this.WithId("CAMPOVALIDACAO_CODE"),
                new Column("TDCAMPO_DOCUMENTO", DbType.Int32),
                new Column("TDCAMPO_DOCUMENTOPRINCIPAL", DbType.Int32));

            this.Database.CreateSequence("SEQ_CAMPOVALIDACAO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CAMPOVALIDACAO");
            this.Database.RemoveSequence("SEQ_CAMPOVALIDACAO");
        }
    }
}