namespace Veros.Paperless.Infra.Filas
{
    using Framework.Servicos;
    using Model.Entidades;

    public class FilaOcrClientePool
    {
        private readonly ObjectPool<FilaOcrCliente> pool;

        public FilaOcrClientePool(int tamanho, ConfiguracaoIp configuracoesDaFila)
        {
            this.pool = new ObjectPool<FilaOcrCliente>(tamanho, x => new FilaOcrCliente(configuracoesDaFila));
        }

        public FilaOcrCliente Obter()
        {
            return this.pool.Get();
        }

        public void Devolver(FilaOcrCliente filaOcrCliente)
        {
            this.pool.Put(filaOcrCliente);
        }
    }
}