namespace Veros.Paperless.Model.Servicos.Autenticacao
{
    using Entidades;
    using Framework.Modelo;
    using Framework.Servicos;
    using Repositorios;

    public class AutenticacaoServico : IAutenticacaoServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public AutenticacaoServico(IUsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public Autenticacao Autenticar(string login, string senha)
        {
            var autenticacao = new Autenticacao(this.usuarioRepositorio.ObterPeloLoginSenha(login, senha));

            if (autenticacao.Autenticou)
            {
                var usuario = (Usuario) autenticacao.Usuario;
                if (usuario.Status == Usuario.StatusExcluido)
                {
                    throw new RegraDeNegocioException("Usuário desativado");
                }
            }

            return autenticacao;
        }

        public IUsuario ObterUsuarioPeloLogin(string login)
        {
            return this.usuarioRepositorio.ObterPeloLogin(login);
        }
    }
}
