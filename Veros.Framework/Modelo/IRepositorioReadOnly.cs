namespace Veros.Framework.Modelo
{
    public interface IRepositorioReadOnly<T> where T : Entidade
    {
        T ObterPorId(int id);

        T[] ObterTodos();
    }
}