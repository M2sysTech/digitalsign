namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class AgenciaRepositorio : Repositorio<Agencia>, IAgenciaRepositorio
    {
        public IList<Agencia> ObterPorBancoId(int bancoId)
        {
            return this.Session.QueryOver<Agencia>()
                .Where(x => x.BancoId == bancoId)
                .List();
        }

        public Agencia ObterPorNumeroAgenciaBancoId(string numeroAgencia, int bancoId)
        {
            return this.Session.QueryOver<Agencia>()
                .Where(x => x.BancoId == bancoId)
                .Where(x => x.Numero == numeroAgencia)
                .Take(1)
                .SingleOrDefault<Agencia>();
        }
    }
}