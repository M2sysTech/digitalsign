namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class LogOperadorRepositorio : Repositorio<LogOperador>, ILogOperadorRepositorio
    {
        public void GravarLogOut(int usuarioId, DateTime horaLogOut)
        {
            var ultimoLogin = this.ObterUltimoLogin(usuarioId);

            if (ultimoLogin == null)
            {
                return;
            }

            this.Session
              .CreateQuery("update LogOperador set HoraLogoff = :hora where Id = :id and Usuario.Id = :idUsuario")
              .SetParameter("hora", horaLogOut)
              .SetParameter("idUsuario", usuarioId)
              .SetParameter("id", ultimoLogin.Id)
              .ExecuteUpdate();
        }

        public IList<LogOperador> ObterPendenciasDeLogout(int usuarioId)
        {
            return this.Session.QueryOver<LogOperador>()
                .Where(x => x.HoraLogoff == null)
                .And(x => x.Usuario.Id == usuarioId)
                .List();
        }

        public LogOperador ObterUltimoLogin(int usuarioId)
        {
            return this.Session.QueryOver<LogOperador>()
                .Where(x => x.HoraLogoff == null)
                .And(x => x.Usuario.Id == usuarioId)
                .List()
                .OrderByDescending(x => x.HoraLogin)
                .FirstOrDefault();
        }

        public void AtualizarUltimoAcesso(int usuarioId, DateTime horaAtual)
        {
            var ultimoLogin = this.ObterUltimoLogin(usuarioId);

            if (ultimoLogin == null )
            {
                return;
            }

            this.Session
              .CreateQuery("update LogOperador set HoraUltimoAcesso = :hora where Id = :id and Usuario.Id = :idUsuario")
              .SetParameter("hora", horaAtual)
              .SetParameter("id", ultimoLogin.Id)
              .SetParameter("idUsuario", usuarioId)
              .ExecuteUpdate();
        }
    }
}
