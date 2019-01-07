namespace Veros.Framework.Servicos
{
    using Modelo;

    /// <summary>
    /// Contrato para serviço de autenticacao de usuário
    /// </summary>
    public interface IAutenticacaoServico
    {
        /// <summary>
        /// Autentica usuário
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>Resultado da autenticacao</returns>
        Autenticacao Autenticar(string login, string senha);

        /// <summary>
        /// Retorna usuario pelo login
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <returns>Usuario do sistema</returns>
        IUsuario ObterUsuarioPeloLogin(string login);
    }
}
