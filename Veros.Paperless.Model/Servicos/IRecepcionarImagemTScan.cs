namespace Veros.Paperless.Model.Servicos
{
    public interface IRecepcionarImagemTScan
    {
        void Execute(
            bool verso,
            string nomeArquivo,
            int id,
            bool upado,
            string conteudoEmBase64,
            int loteId,
            string token,
            string hash);

        string CaminhoDiretorio(int loteId);
    }
}