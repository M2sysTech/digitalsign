namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Entidades;
    using Model.Repositorios;

    public class ArquivoPackRepositorio : Repositorio<ArquivoPack>, IArquivoPackRepositorio
    {
        public IList<ArquivoPack> ObterComTimeout()
        {
            return this.Session.QueryOver<ArquivoPack>()
                .Where(x => x.Status == ArquivoPackStatus.TimeOut)
                .List();
        }

        public void AtualizaStatus(int arquivoPackId, ArquivoPackStatus arquivoPackStatus)
        {
            this.Session
               .CreateQuery("update ArquivoPack set Status = :status where Id = :id")
               .SetInt32("id", arquivoPackId)
               .SetParameter("status", arquivoPackStatus)
               .ExecuteUpdate();
        }
    }
}