namespace Veros.Framework.Servicos
{
    using Modelo;

    /// <summary>
    /// Representa o resultado de uma autentica��o
    /// </summary>
    public class Autenticacao
    {
        /// <summary>
        /// Retorna uma autentica��o negada
        /// </summary>
        public static Autenticacao Negado = new Autenticacao(null);

        public Autenticacao(IUsuario usuario)
        {
            this.Autenticou = usuario != null;
            this.Usuario = usuario;
        }

        /// <summary>
        /// Retorna true se autenticou com sucesso e false se n�o autenticou
        /// </summary>
        public bool Autenticou
        {
            get;
            private set;
        }

        /// <summary>
        /// Retorna usu�rio que foi retornado pela autentica��o
        /// </summary>
        public IUsuario Usuario
        {
            get;
            private set;
        }
    }
}