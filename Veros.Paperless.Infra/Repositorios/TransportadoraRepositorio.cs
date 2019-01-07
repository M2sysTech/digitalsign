namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class TransportadoraRepositorio : Repositorio<Transportadora>, ITransportadoraRepositorio
    {        
        public Transportadora ObterPeloCnpj(string cnpj)
        {
            return this.Session.QueryOver<Transportadora>()
                .Where(x => x.Cnpj == cnpj)
                .Take(1)
                .SingleOrDefault();
        }

        public IList<Transportadora> ObterAtivas()
        {
            return this.Session.QueryOver<Transportadora>()
                .Where(x => x.Status == Transportadora.StatusAtivo)
                .List();
        }

        public void AlterarStatus(int transportadoraId, string status)
        {
            this.Session
              .CreateQuery("update Transportadora set Status = :status where Id = :id")
              .SetInt32("id", transportadoraId)
              .SetString("status", status)
              .ExecuteUpdate();
        }
    }
}
