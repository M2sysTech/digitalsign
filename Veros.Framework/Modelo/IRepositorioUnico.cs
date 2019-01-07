namespace Veros.Framework.Modelo
{
    /// <summary>
    /// Inteface que implementa um repositório de um certo.
    /// </summary>
    /// <typeparam name="T">Um dos tipos especificados</typeparam>
    public interface IRepositorioUnico<T> : IRepositorio where T : EntidadeUnica
    {
        /// <summary>
        /// Salva um item no repositório
        /// </summary>
        /// <param name="item">Item a ser salvo</param>
        void Salvar(T item);

        /// <summary>
        /// Retorna um item de acordo com seu ID
        /// </summary>
        /// <returns>Objeto pretendido</returns>
        T Obter();
    }
}