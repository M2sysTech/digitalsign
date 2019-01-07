namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Usuario ObterPeloLoginSenha(
            string login,
            string senha);

        Usuario ObterPeloLogin(string login);

        IList<Usuario> ObterUsuariosWeb();

        IList<Usuario> ObterUsuariosComPerfil();

        Usuario ObterPorToken(string scheme);

        bool ExisteUsuarioPorPerfil(int perfilId);

        void AlterarStatus(int usuarioId, string status);

        void AlterarSenha(int usuarioId, string senhaNova);

        void AlterarSenha(int usuarioId, string senhaNova, string forcarAlteracaoAoLogar);

        void GravarNomeFoto(int usuarioId, string nomeArquivo);

        void AtualizarToken(int id, string chave);

        void RegistrarLogin(int usuarioId);
        
        void AtualizarUltimoAcesso(int usuarioId);

        void GravarLogof(int usuarioId);
    }
}
