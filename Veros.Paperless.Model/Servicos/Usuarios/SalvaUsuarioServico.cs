namespace Veros.Paperless.Model.Servicos.Usuarios
{
    using System;
    using Entidades;
    using Framework.Security;
    using Veros.Paperless.Model.Repositorios;

    public class SalvaUsuarioServico : ISalvaUsuarioServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly ISalvaFotoUsuarioServico salvaFotoUsuarioServico;

        public SalvaUsuarioServico(
            IUsuarioRepositorio usuarioRepositorio,
            ISalvaFotoUsuarioServico salvaFotoUsuarioServico)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.salvaFotoUsuarioServico = salvaFotoUsuarioServico;
        }

        public Usuario Salvar(Usuario usuario)
        {
            if (this.LoginDuplicado(usuario))
            {
                return usuario;
            }

            if (usuario.Id > 1)
            {
                this.salvaFotoUsuarioServico.Salvar(usuario);
                return this.Alterar(usuario);
            }

            return this.Incluir(usuario);
        }
        
        private Usuario Incluir(Usuario usuario)
        {
            if (this.PodeSalvar(usuario) == false)
            {
                return usuario;
            }

            usuario.Tipo = "W";
            usuario.Senha = new UpperCaseHash().HashText(usuario.Senha.ToUpper());

            if (string.IsNullOrEmpty(usuario.Nome))
            {
                usuario.Nome = usuario.Login;
            }

            this.usuarioRepositorio.Salvar(usuario);
            this.salvaFotoUsuarioServico.Salvar(usuario);

            return usuario;
        }

        private Usuario Alterar(Usuario usuario)
        {
            var usuarioDoBanco = this.usuarioRepositorio.ObterPorId(usuario.Id);

            if (usuarioDoBanco == null)
            {
                throw new NullReferenceException("C�digo de usu�rio n�o existe na base.");
            }

            if (string.IsNullOrEmpty(usuario.Login) == false)
            {
                usuarioDoBanco.Login = usuario.Login;    
            }

            if (string.IsNullOrEmpty(usuario.Senha) == false)
            {
                usuarioDoBanco.Senha = new UpperCaseHash().HashText(usuario.Senha.ToUpper());    
            }

            if (usuario.Perfil != null && usuario.Perfil.Id > 0)
            {
                usuarioDoBanco.Perfil = usuario.Perfil;    
            }

            if (usuario.Loja != null && usuario.Loja.Id > 0)
            {
                usuarioDoBanco.Loja = usuario.Loja;
            }

            this.usuarioRepositorio.Salvar(usuarioDoBanco);
            return usuarioDoBanco;
        }

        private bool LoginDuplicado(Usuario usuario)
        {
            var usuarioDoBanco = this.usuarioRepositorio.ObterPeloLogin(usuario.Login);

            if (usuarioDoBanco == null || usuarioDoBanco.Id == 0)
            {
                return false;
            }

            if (usuarioDoBanco.Id != usuario.Id)
            {
                throw new NullReferenceException("Login j� est� cadastrado.");
            }

            return false;
        }

        private bool PodeSalvar(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Login))
            {
                throw new NullReferenceException("Login n�o foi informado.");
            }

            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new NullReferenceException("Senha n�o foi informada.");
            }

            if (usuario.Perfil == null || usuario.Perfil.Id < 1)
            {
                throw new NullReferenceException("Perfil n�o foi informado.");
            }

            return true;
        }
    }
}