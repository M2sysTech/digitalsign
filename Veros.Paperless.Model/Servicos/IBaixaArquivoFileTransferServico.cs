namespace Veros.Paperless.Model.Servicos
{
    public interface IBaixaArquivoFileTransferServico
    {
        string BaixarArquivo(int id, string fileType, Veros.Paperless.Model.Entidades.FileTransfer fileTransfer, bool usarCache = true, bool baixarThumbnail = false);

        string BaixarArquivo(int id, string fileType, bool baixarArquivoOriginal = false, bool manterNomeOriginal = false, bool usarCache = true, int dataCenterId = -1);

        string BaixarArquivoGenerico(string caminhoFileTransfer, string caminhoDestinoLocal, int dataCenterId = -1);

        string BaixarPacCortada(int documentoId);

        string BaixarFotoUsuario(string nomeArquivo);

        string BaixarPadraoDeFace(int faceId, string nomeArquivoTxt);

        string BaixarArquivoNaPasta(int id, string fileType, string pathDestino, bool baixarArquivoOriginal = false, int dataCenterId = -1);

        string BaixarCertificadoGarantia(string nomeArquivoOrigem, string caminhoDestinoLocal);
    }
}
