namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface ITabelaItensRepositorio : IRepositorio<TabelaItens>
    {
        IList<TabelaItens> ObterPorIdentificador(string identificador);
    }
}