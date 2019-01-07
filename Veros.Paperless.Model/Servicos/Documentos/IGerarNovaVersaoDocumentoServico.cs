namespace Veros.Paperless.Model.Servicos.Documentos
{
    public interface IGerarNovaVersaoDocumentoServico
    {
        void Executar(int documentoId, string caminhoImagens);
    }
}
