namespace Veros.Paperless.Infra.Storage.Amazon
{
    using System;
    using Framework;
    using Framework.IO;
    using Model.Storages;
    using Veros.Paperless.Model.Entidades;

    public class BaixarArquivoAmazonS3 : IBaixarArquivoAmazonS3
    {
        private readonly AmazonStorage amazonStorage;

        public BaixarArquivoAmazonS3(AmazonStorage amazonStorage)
        {
            this.amazonStorage = amazonStorage;
        }

        public string ObterUrl(Pagina pagina)
        {
            var remoteDirectory = Files.GetEcmPath(pagina.Id).ToUpper().Replace("\\", "/") + "/F";

            var remoteFileName = pagina.Id.ToString("000000000") + "." + pagina.TipoArquivo;

            var inicio = DateTime.Now;
            var url = this.amazonStorage.ObterUrl(remoteDirectory, remoteFileName);
            var fim = DateTime.Now;

            var tempo = fim.Subtract(inicio);

            Log.Application.DebugFormat(
                "Url da pagina #{0} recuperada em {1}ms",
                pagina.Id,
                tempo.TotalMilliseconds);

            return url;
        }

        public void BaixarArquivo(Pagina pagina, string caminhoArquivoDestino)
        {
            var remoteDirectory = Files.GetEcmPath(pagina.Id).ToUpper().Replace("\\", "/") + "/F";

            var remoteFileName = pagina.Id.ToString("000000000") + "." + pagina.TipoArquivo;

            var inicio = DateTime.Now;
            this.amazonStorage.Download(remoteDirectory, remoteFileName, caminhoArquivoDestino);
            var fim = DateTime.Now;

            var tempo = fim.Subtract(inicio);

            Log.Application.DebugFormat(
                "Download da pagina #{0} recuperada em {1}ms",
                pagina.Id,
                tempo.TotalMilliseconds);
        }
    }
}