namespace Veros.Framework.Modelo
{
    public interface IRepositorioDeDicionario<T> where T : EntidadeDicionario
    {
        void Apagar(string chave);

        void Salvar(string chave, string valor);

        string ObterValor(string chave);

        bool ObterValorBoolean(string chave);

        T[] ObterTodos();
    }
}
