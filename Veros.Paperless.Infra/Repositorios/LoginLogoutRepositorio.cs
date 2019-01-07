namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class LoginLogoutRepositorio : Repositorio<LoginLogout>, ILoginLogoutRepositorio
    {
        public void GravarLogoutDosPendentes(int usuarioId, DateTime data)
        {
            this.Session
              .CreateQuery(@"
update LoginLogout set DataLogout = :data
where Usuario.Id = :usuarioId
and DataLogout = null
")
              .SetParameter("usuarioId", usuarioId)
              .SetParameter("data", data)
              .ExecuteUpdate();
        }
    }
}