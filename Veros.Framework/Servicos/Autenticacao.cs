namespace Veros.Framework.Servicos
{
    using Modelo;

    /// <summary>
    /// Representa o resultado de uma autenticação
    /// </summary>
    public class Autenticacao
    {
        /// <summary>
        /// Retorna uma autenticação negada
        /// </summary>
        public static Autenticacao Negado = new Autenticacao(null);

        public Autenticacao(IUsuario usuario)
        {
            this.Autenticou = usuario != null;
            this.Usuario = usuario;
        }

        /// <summary>
        /// Retorna true se autenticou com sucesso e false se não autenticou
        /// </summary>
        public bool Autenticou
        {
            get;
            private set;
        }

        /// <summary>
        /// Retorna usuário que foi retornado pela autenticação
        /// </summary>
        public IUsuario Usuario
        {
            get;
            private set;
        }
    }
}