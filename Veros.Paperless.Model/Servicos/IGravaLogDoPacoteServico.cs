namespace Veros.Paperless.Model.Servicos
{
    public interface IGravaLogDoPacoteServico
    {
        void Executar(string acao,
            int pacoteId,
            string observacao);
    }
}