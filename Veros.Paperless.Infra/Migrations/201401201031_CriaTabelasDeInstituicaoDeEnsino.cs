namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201401201031)]
    public class CriaTabelasDeInstituicaoDeEnsino : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "instituicaoensino",
                this.WithId("instituicaoensino_code"),
                new Column("instituicaoensino_name", DbType.String, 254));

            this.Database.CreateSequence("seq_instituicaoensino");

            this.Database.AddTable(
                "ieendereco",
                this.WithId("ieendereco_code"),
                new Column("instituicaoensino_code", DbType.Int32),
                new Column("ieendereco_desc", DbType.String, 254));

            this.Database.CreateSequence("seq_ieendereco");

            this.Database.AddTable(
                "curso",
                this.WithId("curso_code"),
                new Column("curso_name", DbType.String, 254));

            this.Database.CreateSequence("seq_curso");

            this.Database.AddTable(
                "ieenderecocurso",
                this.WithId("ieenderecocurso_code"),
                new Column("ieendereco_code", DbType.Int32),
                new Column("curso_code", DbType.Int32));

            this.Database.CreateSequence("seq_ieenderecocurso");
        }

        public override void Down()
        {
            this.Database.RemoveTable("instituicaoensino");
            this.Database.RemoveSequence("seq_instituicaoensino");

            this.Database.RemoveTable("ieendereco");
            this.Database.RemoveSequence("seq_ieendereco");
            
            this.Database.RemoveTable("curso");
            this.Database.RemoveSequence("seq_curso");

            this.Database.RemoveTable("ieenderecocurso");
            this.Database.RemoveSequence("seq_ieenderecocurso");
        }
    }
}
