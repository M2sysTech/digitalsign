namespace Veros.Paperless.Infra.Repositorios
{
    using System.Linq;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class LogBatimentoRepositorio : Repositorio<LogBatimento>, ILogBatimentoRepositorio
    {
        public LogBatimento ObterPorIndexacao(Indexacao indexacao)
        {
            return this.Session.QueryOver<LogBatimento>()
                .Where(x => x.Indexacao == indexacao)
                .List()
                .FirstOrDefault();
        }
    }
}