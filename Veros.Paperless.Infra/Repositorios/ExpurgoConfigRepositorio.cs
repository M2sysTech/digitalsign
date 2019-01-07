namespace Veros.Paperless.Infra.Repositorios
{
    using Data.Hibernate;
    using Model.Entidades;
    using Model.Repositorios;

    public class ExpurgoConfigRepositorio : RepositorioUnico<ExpurgoConfig>, IExpurgoConfigRepositorio
    {
        public void RegistrarUltimoExpurgoBk()
        {
            this.Session
              .CreateQuery("update ExpurgoConfig set UltimoExpurgoBk = sysdate")
              .ExecuteUpdate();
        }

        public void RegistrarUltimoExpurgoHistorico()
        {
            this.Session
              .CreateQuery("update ExpurgoConfig set UltimoExpurgo = sysdate")
              .ExecuteUpdate();
        }
    }
}
