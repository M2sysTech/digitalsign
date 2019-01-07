namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class EnderecoRepositorio : Repositorio<Endereco>, IEnderecoRepositorio
    {
        public IList<Endereco> ObterTodosPorCep(string cep)
        {
            return this.Session.QueryOver<Endereco>()
                .Where(x => x.Cep == cep)
                .List<Endereco>();
        }
    }
}
