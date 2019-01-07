namespace Veros.Paperless.Model.Servicos.FileTransferBalance
{
    using Entidades;
    using Repositorios;

    public class ObtemFileTransferDaPaginaServico : IObtemFileTransferDaPaginaServico
    {
        private readonly IConfiguracaoIpRepositorio configuracaoIpRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;

        public ObtemFileTransferDaPaginaServico(
            IConfiguracaoIpRepositorio configuracaoIpRepositorio, 
            IPaginaRepositorio paginaRepositorio)
        {
            this.configuracaoIpRepositorio = configuracaoIpRepositorio;
            this.paginaRepositorio = paginaRepositorio;
        }

        public FileTransfer Obter(int paginaId, int dataCenterId)
        {
            if (dataCenterId > -1)
            {
                return new FileTransfer
                {
                    ConfiguracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer(dataCenterId)
                };
            }

            var pagina = this.paginaRepositorio.ObterPorId(paginaId);

            if (pagina != null && pagina.Id > 0)
            {
                return new FileTransfer
                {
                    ConfiguracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer(pagina.DataCenter)
                };
            }

            return new FileTransfer
            {
                ConfiguracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoFileTransfer()
            };
        }
    }
}
