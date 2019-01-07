namespace Veros.Paperless.Infra.Filas
{
    public class FilaComparacao : IFilaComparacao
    {
        public int ObterFaceId()
        {
            var cliente = FilaComparacaoCliente.Pool.Obter();

            try
            {
                return cliente.ObterFaceId();
            }
            finally
            {
                FilaComparacaoCliente.Pool.Devolver(cliente);
            }
        }
    }
}