namespace Veros.Paperless.Model.Servicos.FileTransferBalance
{
    using System.Linq;
    using System.IO;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Modelo;
    using Veros.Paperless.Model.Repositorios;

    public class ObtemFileTransferParaArquivoServico : IObtemFileTransferParaArquivoServico
    {
        private readonly IFileTransferRepositorio fileTransferRepositorio;
        private readonly IConfiguracaoIpRepositorio configuracaoIpRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;

        public ObtemFileTransferParaArquivoServico(
            IFileTransferRepositorio fileTransferRepositorio, 
            IConfiguracaoIpRepositorio configuracaoIpRepositorio, 
            IPaginaRepositorio paginaRepositorio)
        {
            this.fileTransferRepositorio = fileTransferRepositorio;
            this.configuracaoIpRepositorio = configuracaoIpRepositorio;
            this.paginaRepositorio = paginaRepositorio;
        }
        
        public FileTransfer Obter(int paginaId, string arquivoLocal)
        {
            if (paginaId > 1)
            {
                var pagina = this.paginaRepositorio.ObterPorId(paginaId);

                if (pagina.Id > 0 && pagina.DataCenter > 0)
                {
                    var configuracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer(pagina.DataCenter);
                    var filesTransfers = this.fileTransferRepositorio.ObterTodos();

                    var fileTransfer = filesTransfers.FirstOrDefault(x => x.Tag == configuracaoIp.Tag);
                    var tamanhoArquivo = new FileInfo(arquivoLocal).Length;
                    
                    if (fileTransfer != null && fileTransfer.AceitaArquivo(tamanhoArquivo))
                    {
                        fileTransfer.ConfiguracaoIp = configuracaoIp;
                        fileTransfer.TamanhoArquivoAtual = tamanhoArquivo;

                        return fileTransfer;
                    }
                }
            }

            return this.Obter(arquivoLocal);
        }

        public FileTransfer ObterMaisRecente()
        {
            var filesTransfers = this.fileTransferRepositorio.ObterTodos();

            if (filesTransfers.Any() == false)
            {
                return new FileTransfer
                {
                    ConfiguracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer(),
                    Tag = ConfiguracaoIp.TagFileTransfer
                };
            }

            return filesTransfers.OrderByDescending(x => x.Tag).FirstOrDefault();
        }

        public FileTransfer Obter(string arquivoLocal)
        {
            var filesTransfers = this.fileTransferRepositorio.ObterTodos();
            
            if (filesTransfers.Any() == false)
            {
                return new FileTransfer
                {
                    ConfiguracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer()
                };
            }
            
            var tamanhoNovoArquivo = new FileInfo(arquivoLocal).Length;

            var fileTransfersM2Sys = filesTransfers
                .Where(x => x.EhCloud == false)
                .OrderBy(x => x.Tag);

            if (fileTransfersM2Sys.Any() == false)
            {
                return new FileTransfer
                {
                    ConfiguracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer()
                };
            }

            foreach (var fileTransfer in fileTransfersM2Sys)
            {
                if (fileTransfer.AceitaArquivo(tamanhoNovoArquivo))
                {
                    fileTransfer.TamanhoArquivoAtual = tamanhoNovoArquivo;
                    fileTransfer.ConfiguracaoIp = this.configuracaoIpRepositorio.ObterPorTag(fileTransfer.Tag);

                    return fileTransfer;
                }
            }

            throw new RegraDeNegocioException("Nenhum filetransfer disponsível!");
        }
    }
}
