namespace Veros.Paperless.Model.Servicos
{
    using Veros.Paperless.Model.Entidades;

    public interface IGravaLogDaPaginaServico
    {
        void Executar(string acaoLogPagina,
            int paginaId,
            int documentoId,
            string observacao);

        void ExecutarNovaThread(string acaoLogPagina,
            int paginaId,
            int documentoId,
            string observacao);
    }
}