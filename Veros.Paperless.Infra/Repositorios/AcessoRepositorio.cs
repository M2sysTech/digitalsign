namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Model.Entidades;
    using Data.Hibernate;
    using Model.Repositorios;

    public class AcessoRepositorio : Repositorio<Acesso>, IAcessoRepositorio
    {
        public IList<Acesso> ObterAcessosPorPerfil(int perfilId)
        {
            return this.Session.QueryOver<Acesso>()
                .Where(x => x.Perfil.Id == perfilId)
                .List();
        }

        public void ApagarPorPerfil(int perfilId)
        {
            this.Session.CreateQuery("delete from Acesso where Perfil.Id = :id")
              .SetInt32("id", perfilId)
              .ExecuteUpdate();
        }

        public bool ExisteAcessoPorPerfilEFuncionalidade(
            int perfilId,
            Funcionalidade funcionalidade)
        {
            return this.Session.QueryOver<Acesso>()
                .Where(x => x.Perfil.Id == perfilId)
                .Where(x => x.Funcionalidade == funcionalidade)
                .List().Count > 0;
        }
    }
}
