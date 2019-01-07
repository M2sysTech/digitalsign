namespace Veros.Paperless.Infra.Filas
{
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Entidades;

    public class FilaComparacaoClientePool
    {
        private readonly ObjectPool<FilaComparacaoCliente> pool;

        public FilaComparacaoClientePool(int tamanho, ConfiguracaoIp configuracoesDaFila)
        {
            this.pool = new ObjectPool<FilaComparacaoCliente>(tamanho, x => new FilaComparacaoCliente(configuracoesDaFila));
        }

        public FilaComparacaoCliente Obter()
        {
            return this.pool.Get();
        }

        public void Devolver(FilaComparacaoCliente filaComparacaoCliente)
        {
            this.pool.Put(filaComparacaoCliente);
        }
    }
}