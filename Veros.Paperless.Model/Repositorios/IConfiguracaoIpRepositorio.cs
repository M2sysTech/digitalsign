namespace Veros.Paperless.Model.Repositorios
{
    using Entidades;
    using Framework.Modelo;

    public interface IConfiguracaoIpRepositorio : IRepositorio<ConfiguracaoIp>
    {
        ConfiguracaoIp ObterConfiguracaoDaFila();

        ConfiguracaoIp ObterConfiguracaoFileTransfer();

        ConfiguracaoIp ObterConfiguracaoFileTransfer(int dataCenterId);

        ConfiguracaoIp ObterPorTag(string tag);

        ConfiguracaoIp ObterConfiguracaoDaFilaDeOcr();
    }
}
