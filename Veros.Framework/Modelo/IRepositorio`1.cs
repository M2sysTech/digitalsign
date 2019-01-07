namespace Veros.Framework.Modelo
{
    /// <summary>
    /// Contrato para repositório de alguma entidade
    /// </summary>
    /// <typeparam name="T">Um dos tipos especificados</typeparam>
    public interface IRepositorio<T> : IRepositorio where T : Entidade
    {
        void Salvar(T item);

        T ObterPorId(int id);

        T[] ObterTodos();

        void Apagar(T item);

        void ApagarPorId(int id);

        void ApagarTodos();
    }
}