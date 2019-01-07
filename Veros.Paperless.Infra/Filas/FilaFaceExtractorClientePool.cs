namespace Veros.Paperless.Infra.Filas
{
    using Framework.Servicos;
    using Model.Entidades;

    public class FilaFaceExtractorClientePool
    {
        private readonly ObjectPool<FilaFaceExtractorCliente> pool;

        public FilaFaceExtractorClientePool(int tamanho, ConfiguracaoIp configuracoesDaFila)
        {
            this.pool = new ObjectPool<FilaFaceExtractorCliente>(tamanho, x => new FilaFaceExtractorCliente(configuracoesDaFila));
        }

        public FilaFaceExtractorCliente Obter()
        {
            return this.pool.Get();
        }

        public void Devolver(FilaFaceExtractorCliente filaFaceExtractorCliente)
        {
            this.pool.Put(filaFaceExtractorCliente);
        }
    }
}