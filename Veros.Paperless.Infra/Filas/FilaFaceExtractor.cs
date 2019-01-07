namespace Veros.Paperless.Infra.Filas
{
    public class FilaFaceExtractor : IFilaFaceExtractor
    {
        public int ObterFaceId()
        {
            var cliente = FilaFaceExtractorCliente.Pool.Obter();

            try
            {
                return cliente.Obter();
            }
            finally
            {
                FilaFaceExtractorCliente.Pool.Devolver(cliente);
            }
        }
    }
}