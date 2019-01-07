namespace Veros.Paperless.Infra.Storage
{
    using System.IO;
    using Model.Entidades;
    using Model.Servicos.FileTransferBalance;
    using Veros.Framework;
    using Veros.Framework.IO;
    using Veros.Paperless.Model.Repositorios;
    using Veros.Paperless.Model.Servicos;
    using Veros.Storage.FileTransfer;

    /// <summary>
    /// TODO: testes
    /// </summary>
    public class PostaArquivoFileTransferServico : IPostaArquivoFileTransferServico
    {
        private readonly IConfiguracaoIpRepositorio cofiguracaoIpRepositorio;
        private readonly IObtemFileTransferParaArquivoServico obtemFileTransferParaArquivoServico;
        private readonly IRegistraFileTransferDaPaginaServico registraFileTransferDaPaginaServico;

        public PostaArquivoFileTransferServico(
            IConfiguracaoIpRepositorio cofiguracaoIpRepositorio, 
            IObtemFileTransferParaArquivoServico obtemFileTransferParaArquivoServico, 
            IRegistraFileTransferDaPaginaServico registraFileTransferDaPaginaServico)
        {
            this.cofiguracaoIpRepositorio = cofiguracaoIpRepositorio;
            this.obtemFileTransferParaArquivoServico = obtemFileTransferParaArquivoServico;
            this.registraFileTransferDaPaginaServico = registraFileTransferDaPaginaServico;
        }

        public void PostarPagina(Pagina pagina, string arquivoLocal)
        {
            var fileTransfer = this.obtemFileTransferParaArquivoServico.Obter(arquivoLocal);

            var extension = Path.GetExtension(arquivoLocal).Replace(".", string.Empty);
            var imagemFileTransfer = this.NomeArquivoNoFileTransfer(pagina.Id, extension);

            this.Postar(arquivoLocal, imagemFileTransfer, fileTransfer.ConfiguracaoIp);
            this.registraFileTransferDaPaginaServico.Executar(fileTransfer, pagina.Id);
            pagina.DataCenter = fileTransfer.ConfiguracaoIp.DataCenterId;
        }

        public void PostarPagina(int paginaId, string fileType, string arquivoLocal)
        {
            var fileTransfer = this.obtemFileTransferParaArquivoServico.Obter(arquivoLocal);

            //// TODO: mudar GetEcmPath para GetM0k0Path. Colocar no framework?
            var imagemFileTransfer = this.NomeArquivoNoFileTransfer(paginaId, fileType);

            this.Postar(arquivoLocal, imagemFileTransfer, fileTransfer.ConfiguracaoIp);
            this.registraFileTransferDaPaginaServico.Executar(fileTransfer, paginaId);
        }

        public void PostarArquivo(int paginaId, string imagemLocal, string caminhoRemoto)
        {
            var fileTransfer = this.obtemFileTransferParaArquivoServico.Obter(paginaId, imagemLocal);
            ////var configuracaoIp = this.cofiguracaoIpRepositorio.ObterConfiguracaoFileTransfer();
            
            this.Postar(imagemLocal, caminhoRemoto, fileTransfer.ConfiguracaoIp);
            this.registraFileTransferDaPaginaServico.Executar(fileTransfer, paginaId);
        }

        public void PostarPacCortada(int documentoId, string imagemJuntada)
        {
            var configuracaoIp = this.cofiguracaoIpRepositorio.ObterConfiguracaoFileTransfer();

            var imagemFileTransfer = string.Format(
              @"/PAC/{0}/F/{1}.TIF",
              Files.GetEcmPath(documentoId),
              documentoId.ToString("000000000")).ToUpper().Replace("\\", "/");

            this.Postar(imagemJuntada, imagemFileTransfer, configuracaoIp);
        }

        public void PostarJson(int loteId, string arquivoJson)
        {
            var configuracaoIp = this.cofiguracaoIpRepositorio.ObterConfiguracaoFileTransfer();

            var arquivoNoFileTransfer = string.Format(
              @"/JSON/{0}/F/{1}.JSON",
              Files.GetEcmPath(loteId),
              loteId.ToString("000000000")).ToUpper().Replace("\\", "/");

            this.Postar(arquivoJson, arquivoNoFileTransfer, configuracaoIp);
        }

        public void PostarFotoUsuario(string nomeArquivo, string imagemLocal)
        {
            var configuracaoIp = this.cofiguracaoIpRepositorio.ObterConfiguracaoFileTransfer();
            var imagemFileTransfer = string.Format(@"/USERFOTO/{0}", nomeArquivo);
            this.Postar(imagemLocal, imagemFileTransfer, configuracaoIp);
        }

        public void PostarCertificadoGarantia(string nomeArquivo, string arquivoLocal)
        {
            var fileTransfer = this.obtemFileTransferParaArquivoServico.Obter(arquivoLocal);
            var imagemFileTransfer = string.Format(@"/CERTIFICADOS/{0}", nomeArquivo);
            this.Postar(arquivoLocal, imagemFileTransfer, fileTransfer.ConfiguracaoIp);
            this.registraFileTransferDaPaginaServico.RegistarConsumoBytes(fileTransfer);
        }

        private void Postar(string arquivoLocal, string imagemFileTransfer, ConfiguracaoIp configuracaoIp)
        {
            using (var fileTransfer = FileTransferCliente.Obter())
            {
                fileTransfer.Conectar(configuracaoIp.Host, configuracaoIp.Porta);
                fileTransfer.Enviar(arquivoLocal, imagemFileTransfer);
            }

            Log.Application.DebugFormat("Filetransfer[{2}]: Enviou arquivo de {0} para {1}", arquivoLocal, imagemFileTransfer, configuracaoIp.DataCenterId);
        }

        private string NomeArquivoNoFileTransfer(int paginaId, string fileType)
        {
            return string.Format(
                @"/{0}/F/{1}.{2}",
                Files.GetEcmPath(paginaId),
                paginaId.ToString("000000000"),
                fileType).ToUpper().Replace("\\", "/");
        }
    }
}