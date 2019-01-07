namespace Veros.Paperless.Model.Servicos.Documentos
{
    public interface IGerarNovaVersaoPaginaServico
    {
        void Executar(int documentoId, int paginaId, string acao, string caminhoImagens, string arquivos);
    }
}
