namespace Veros.Framework.Modelo
{
    public interface IUsuario : IEntidade
    {
        string Nome
        {
            get;
            set;
        }

        string Senha
        {
            get;
            set;
        }

        string Login
        {
            get;
            set;
        }
    }
}
