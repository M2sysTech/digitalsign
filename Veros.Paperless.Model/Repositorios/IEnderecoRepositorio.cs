namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IEnderecoRepositorio : IRepositorio<Endereco>
    {
        IList<Endereco> ObterTodosPorCep(string cep);
    }
}
