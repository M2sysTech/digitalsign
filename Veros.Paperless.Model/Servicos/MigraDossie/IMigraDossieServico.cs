namespace Veros.Paperless.Model.Servicos.MigraDossie
{
    public interface IMigraDossieServico
    {
        void Executar(int dossieId, string caixaIdentificacao);
    }
}
