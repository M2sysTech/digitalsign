namespace Veros.Paperless.Infra.Storage
{
    using Framework.IO;
    using Model.Servicos.FileTransferBalance;
    using Veros.Paperless.Model.Servicos;
    using Veros.Storage.FileTransfer;
    using Veros.Paperless.Model.Repositorios;

    /// <summary>
    /// TODO: testes
    /// </summary>
    public class ApagaArquivoFileTransferServico : IApagaArquivoFileTransferServico
    {
        private readonly IConfiguracaoIpRepositorio configuracaoIpRepositorio;
        private readonly IObtemFileTransferDaPaginaServico obtemFileTransferDaPaginaServico;

        public ApagaArquivoFileTransferServico(
            IConfiguracaoIpRepositorio configuracaoIpRepositorio, IObtemFileTransferDaPaginaServico obtemFileTransferDaPaginaServico)
        {
            this.configuracaoIpRepositorio = configuracaoIpRepositorio;
            this.obtemFileTransferDaPaginaServico = obtemFileTransferDaPaginaServico;
        }

        public void ApagarArquivo(int id, string fileType)
        {
            var fileTransferDoArquivo = this.obtemFileTransferDaPaginaServico.Obter(id, -1);
            ////var configuracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer();

            var imagemFileTransfer = string.Format(
                 @"/{0}/F/{1}.{2}",
                 Files.GetEcmPath(id),
                 id.ToString("000000000"),
                 fileType).ToUpper().Replace("\\", "/");

            using (var fileTransfer = FileTransferCliente.Obter())
            {
                fileTransfer.Conectar(fileTransferDoArquivo.ConfiguracaoIp.Host, fileTransferDoArquivo.ConfiguracaoIp.Porta);
                fileTransfer.Apagar(imagemFileTransfer);
            }
        }
    }
}
