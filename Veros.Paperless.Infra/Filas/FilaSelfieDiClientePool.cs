namespace Veros.Paperless.Infra.Filas
{
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Entidades;

    public class FilaSelfieDiClientePool
    {
        private readonly ObjectPool<FilaSelfieDiCliente> pool;

        public FilaSelfieDiClientePool(int tamanho, ConfiguracaoIp configuracoesDaFila)
        {
            this.pool = new ObjectPool<FilaSelfieDiCliente>(tamanho, x => new FilaSelfieDiCliente(configuracoesDaFila));
        }

        public FilaSelfieDiCliente Obter()
        {
            return this.pool.Get();
        }

        public void Devolver(FilaSelfieDiCliente filaSelfieDiCliente)
        {
            this.pool.Put(filaSelfieDiCliente);
        }
    }
}