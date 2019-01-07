namespace Veros.Paperless.Infra.Storage
{
    using System.IO;
    using Amazon;
    using Model.Entidades;
    using Model.Repositorios;
    using Model.Servicos.FileTransferBalance;
    using Veros.Framework;
    using Veros.Framework.IO;
    using Veros.Paperless.Model.Servicos;
    
    public class PostaArquivoAmazonS3 : IPostaArquivoAmazonS3
    {
        private readonly IFileTransferRepositorio filetransferRepositorio;
        private readonly IRegistraFileTransferDaPaginaServico registraFileTransferDaPaginaServico;
        private readonly IConfiguracaoIpRepositorio configuracaoIpRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;

        public PostaArquivoAmazonS3(
            IFileTransferRepositorio filetransferRepositorio, 
            IRegistraFileTransferDaPaginaServico registraFileTransferDaPaginaServico, 
            IConfiguracaoIpRepositorio configuracaoIpRepositorio, 
            IPaginaRepositorio paginaRepositorio)
        {
            this.filetransferRepositorio = filetransferRepositorio;
            this.registraFileTransferDaPaginaServico = registraFileTransferDaPaginaServico;
            this.configuracaoIpRepositorio = configuracaoIpRepositorio;
            this.paginaRepositorio = paginaRepositorio;
        }

        public void PostarPagina(Pagina pagina, string arquivoLocal)
        {
            var fileTransfer = this.filetransferRepositorio.ObterCloud();

            var remoteDirectory = Files.GetEcmPath(pagina.Id).ToUpper().Replace("\\", "/") + "/F";

            var extension = Path.GetExtension(arquivoLocal).ToUpper();
            var remoteFileName = pagina.Id.ToString("000000000") + extension;

            this.Postar(arquivoLocal, remoteDirectory, remoteFileName);

            var configuracaoIp = this.configuracaoIpRepositorio.ObterPorTag(fileTransfer.Tag);
            fileTransfer.ConfiguracaoIp = configuracaoIp;

            this.registraFileTransferDaPaginaServico.Executar(fileTransfer, pagina.Id);
            this.paginaRepositorio.AtualizarDataCenterAntesCloud(pagina.Id, pagina.DataCenter);
            
            pagina.DataCenterAntesCloud = pagina.DataCenter;
            pagina.DataCenter = fileTransfer.ConfiguracaoIp.DataCenterId;
        }

        private void Postar(string arquivoLocal, string remoteDirectory, string remoteFileName)
        {
            var amazonStorage = new AmazonStorage();
            amazonStorage.Postar(arquivoLocal, remoteDirectory, remoteFileName);

            Log.Application.DebugFormat(
                "AmazonStorage: Enviou arquivo de {0} para {1}",
                arquivoLocal, 
                remoteDirectory);
        }
    }
}