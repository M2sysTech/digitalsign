namespace Veros.Paperless.Model.Servicos
{
    using Veros.Paperless.Model.Entidades;

    public interface IGravaLogDoDocumentoServico
    {
        void Executar(string acaoLogDocumento,
            int documentoId,
            string observacao);
    }
}