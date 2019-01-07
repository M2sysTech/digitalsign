namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Veros.Data.Hibernate;
    using Veros.Framework.Security;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        public Usuario ObterPeloLoginSenha(
            string login,
            string senha)
        {
            return this.Session.QueryOver<Usuario>()
                .Where(x => x.Login == login)
                .Where(x => x.Senha == new UpperCaseHash().HashText(senha.ToUpper()))
                .Take(1)
                .SingleOrDefault();
        }

        public Usuario ObterPeloLogin(string login)
        {
            return this.Session.QueryOver<Usuario>()
                .Where(x => x.Login == login)
                .Take(1)
                .SingleOrDefault();
        }

        public IList<Usuario> ObterUsuariosWeb()
        {
            return this.Session.QueryOver<Usuario>()
                .Where(x => x.Tipo == "W")
                .OrderBy(x => x.Nome).Asc.List();
        }

        public IList<Usuario> ObterUsuariosComPerfil()
        {
            return this.Session.QueryOver<Usuario>()
                .Where(x => x.Status != "*")
                .Fetch(x => x.Perfil).Eager
                .OrderBy(x => x.Nome).Asc.List();
        }

        public Usuario ObterPorToken(string token)
        {
            return this.Session.QueryOver<Usuario>()
                .Where(x => x.Token == token)
                .SingleOrDefault();
        }

        public bool ExisteUsuarioPorPerfil(int perfilId)
        {
            return this.Session.QueryOver<Usuario>()
                .Where(x => x.Perfil.Id == perfilId).List().Count > 0;
        }

        public void AlterarStatus(int usuarioId, string status)
        {
            this.Session
              .CreateQuery("update Usuario set Status = :status where Id = :id")
              .SetInt32("id", usuarioId)
              .SetString("status", status)
              .ExecuteUpdate();
        }

        public void AlterarSenha(int usuarioId, string senhaNova)
        {
            this.Session
             .CreateQuery("update Usuario set Senha = :senhaNova, ForcarAlteracaoSenha = null where Id = :id")
             .SetInt32("id", usuarioId)
             .SetString("senhaNova", new UpperCaseHash().HashText(senhaNova.ToUpper()))
             .ExecuteUpdate();
        }

        public void AlterarSenha(int usuarioId, string senhaNova, string forcarAlteracaoAoLogar)
        {
            this.Session
             .CreateQuery("update Usuario set Senha = :senhaNova, ForcarAlteracaoSenha = :flagForcar where Id = :id")
             .SetInt32("id", usuarioId)
             .SetString("senhaNova", new UpperCaseHash().HashText(senhaNova.ToUpper()))
             .SetString("flagForcar", forcarAlteracaoAoLogar)
             .ExecuteUpdate();
        }

        public void GravarNomeFoto(int usuarioId, string nomeArquivo)
        {
            this.Session
             .CreateQuery("update Usuario set NomeArquivoFoto = :nomeArquivo where Id = :id")
             .SetInt32("id", usuarioId)
             .SetString("nomeArquivo", nomeArquivo)
             .ExecuteUpdate();
        }

        public void AtualizarToken(int usuarioId, string chave)
        {
            this.Session
            .CreateQuery("update Usuario set Token = :token where Id = :id")
            .SetInt32("id", usuarioId)
            .SetString("token", chave)
            .ExecuteUpdate();
        }

        public void RegistrarLogin(int usuarioId)
        {
            this.Session
             .CreateQuery("update Usuario set DataUltimoLogin = :dataUltimoLogin, UltimoAcesso = :ultimoAcesso, EstaLogado = 'S' where Id = :usuarioId")
             .SetParameter("dataUltimoLogin", DateTime.Now)
             .SetParameter("ultimoAcesso", DateTime.Now)
             .SetInt32("usuarioId", usuarioId)
             .ExecuteUpdate();
        }

        public void AtualizarUltimoAcesso(int usuarioId)
        {
            this.Session
             .CreateQuery("update Usuario set EstaLogado = 'S', UltimoAcesso = :ultimoAcesso where Id = :usuarioId")
             .SetParameter("ultimoAcesso", DateTime.Now)
             .SetParameter("usuarioId", usuarioId)
             .ExecuteUpdate();
        }

        public void GravarLogof(int usuarioId)
        {
            this.Session
             .CreateQuery("update Usuario set EstaLogado = 'N' where Id = :usuarioId")
             .SetInt32("usuarioId", usuarioId)
             .ExecuteUpdate();
        }
    }
}
