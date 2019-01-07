namespace Veros.Paperless.Model.Servicos.Documentos
{
    public interface IGerarNovoDocumentoServico
    {
        void Executar(int loteId, int tipoDocumentoId, string caminhoImagens);

        void GerarArquivoUpload(int loteId, int tipoDocumentoId, string caminhoImagens, int documentoPai = 0);
    }
}
