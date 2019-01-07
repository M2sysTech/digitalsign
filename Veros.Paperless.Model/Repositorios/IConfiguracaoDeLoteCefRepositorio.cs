namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IConfiguracaoDeLoteCefRepositorio : IRepositorio<ConfiguracaoDeLoteCef>
    {
        ConfiguracaoDeLoteCef ObterUnico();
    }
}