namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201804030900)]
    public class InsereTipoOcorrencia : Migration
    {
        public override void Up()
        {
            this.AdicionarDossieNaoCadastrado();
            this.AdicionarOutros();
        }

        public override void Down()
        {
        }

        private void AdicionarDossieNaoCadastrado()
        {
            var count = this.Database.ExecuteScalar("Select Count(*) From TipoOcorrencia Where TipoOcorrencia_Nome ='Dossiê Não Cadastrado'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
            Insert Into TipoOcorrencia 
                (TipoOcorrencia_Code, TipoOcorrencia_Nome)
            Values
                (Seq_TipoOcorrencia.NextVal, 'Dossiê Não Cadastrado')");
            }
        }

        private void AdicionarOutros()
        {
            var count = this.Database.ExecuteScalar("Select Count(*) From TipoOcorrencia Where TipoOcorrencia_Nome ='Outros'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
            Insert Into TipoOcorrencia 
                (TipoOcorrencia_Code, TipoOcorrencia_Nome)
            Values
                (Seq_TipoOcorrencia.NextVal, 'Outros')");
            }
        }
    }
}