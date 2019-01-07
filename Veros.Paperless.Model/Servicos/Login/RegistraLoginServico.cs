namespace Veros.Paperless.Model.Servicos.Login
{
    using System;
    using Entidades;
    using Repositorios;

    public class RegistraLoginServico : IRegistraLoginServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly ILoginLogoutRepositorio loginLogoutRepositorio;

        public RegistraLoginServico(
            IUsuarioRepositorio usuarioRepositorio,
            ILoginLogoutRepositorio loginLogoutRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.loginLogoutRepositorio = loginLogoutRepositorio;
        }

        public void Executar(int usuarioId, string maquina)
        {
            var usuario = this.usuarioRepositorio.ObterPorId(usuarioId);

            this.loginLogoutRepositorio.GravarLogoutDosPendentes(usuario.Id, usuario.UltimoAcesso.GetValueOrDefault());

            var loginLogout = new LoginLogout
            {
                Usuario = usuario,
                DataLogin = DateTime.Now,
                Maquina = maquina
            };

            this.loginLogoutRepositorio.Salvar(loginLogout);
        }
    }
}
