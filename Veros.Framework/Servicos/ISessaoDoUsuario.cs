namespace Veros.Framework.Servicos
{
    using Modelo;

    /// <summary>
    /// Contrato para sessão de usuário
    /// </summary>
    public interface ISessaoDoUsuario
    {
        IUsuario UsuarioAtual { get; }

        string Estacao { get; }

        string Nome { get; }

        bool EstaAutenticado { get; }

        void LogIn(IUsuario usuario, bool remember);

        void LogOut();
    }
}