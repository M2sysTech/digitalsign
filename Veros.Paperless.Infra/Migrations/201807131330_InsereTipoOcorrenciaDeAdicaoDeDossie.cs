namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201807131330)]
    public class InsereTipoOcorrenciaDeAdicaoDeDossie : Migration
    {
        public override void Up()
        {
            this.AdicionarDossieAMais();
        }

        public override void Down()
        {
        }

        private void AdicionarDossieAMais()
        {
            var count = this.Database.ExecuteScalar("Select Count(*) From TipoOcorrencia Where TipoOcorrencia_Nome ='Dossiê Adicionado'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
            Insert Into TipoOcorrencia 
                (TipoOcorrencia_Code, TipoOcorrencia_Nome)
            Values
                (43, 'Dossiê Adicionado')");
            }
        }        
    }
}