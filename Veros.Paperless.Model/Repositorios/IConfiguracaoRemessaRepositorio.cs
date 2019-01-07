namespace Veros.Paperless.Model.Repositorios
{
    using Entidades;
    using Framework.Modelo;

    public interface IConfiguracaoRemessaRepositorio : IRepositorio<ConfiguracaoRemessa>
    {
        ConfiguracaoRemessa ObterPorTipoProcesso(int tipoDeProcesso);
    }
}
