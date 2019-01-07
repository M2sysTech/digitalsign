namespace Veros.Paperless.Model.Filas
{
    public interface IFilaClienteGenerica
    {
        int ObterProximo(string nomeDaFila);
        
        int ObterProximo(string nomeDaFila, int parametro);

        int ObterProximo(string nomeDaFila, string parametro);
    }
}
