namespace Veros.Paperless.Model.Servicos
{
    public interface IGravaLogDoLoteServico
    {
        void Executar(string acaoLogLote, int loteId, string observacao, string token);

        void Executar(string acaoLogLote, int loteId, string observacao);
    }
}