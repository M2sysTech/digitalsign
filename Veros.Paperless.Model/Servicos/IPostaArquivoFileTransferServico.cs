namespace Veros.Paperless.Model.Servicos
{
    using Entidades;

    public interface IPostaArquivoFileTransferServico
    {
        void PostarPagina(Pagina pagina, string arquivoLocal);

        void PostarPagina(int paginaId, string fileType, string arquivoLocal);

        void PostarArquivo(int paginaId, string imagemLocal, string caminhoRemoto);

        void PostarPacCortada(int documentoId, string imagemJuntada);

        void PostarJson(int loteId, string arquivoJson);

        void PostarFotoUsuario(string nomeArquivo, string imagemLocal);

        void PostarCertificadoGarantia(string nomeArquivo, string imagemLocal);
    }
}
