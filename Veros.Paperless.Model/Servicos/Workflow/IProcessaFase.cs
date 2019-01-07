namespace Veros.Paperless.Model.Servicos.Workflow
{
    /// <summary>
    /// TODO: APAGAR
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProcessaFase<T> where T : class
    {
        void Processar(T item, bool faseEstaAtiva);
    }
}