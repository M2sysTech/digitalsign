namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IBancoRepositorio : IRepositorio<Banco>
    {
        IList<Banco> ObterTodosPorOrdemDeNome();
    }
}