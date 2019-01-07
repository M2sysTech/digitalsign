namespace Veros.Paperless.Model.Repositorios
{
    using System;
    using Entidades;
    using Framework.Modelo;

    public interface ILogPaginaRepositorio : IRepositorio<LogPagina>
    {
        int ObterPorPagina(int paginaId);
    }
}
