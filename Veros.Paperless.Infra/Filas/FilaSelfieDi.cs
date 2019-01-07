namespace Veros.Paperless.Infra.Filas
{
    public class FilaSelfieDi : IFilaSelfieDi
    {
        public int ObterLoteId()
        {
            var cliente = FilaSelfieDiCliente.Pool.Obter();

            try
            {
                return cliente.ObterLoteId();
            }
            finally
            {
                FilaSelfieDiCliente.Pool.Devolver(cliente);
            }
        }
    }
}