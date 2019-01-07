namespace Veros.Paperless.Infra.Filas
{
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Entidades;
    using Veros.Queues.Client;

    public class FilaSelfieDiCliente : IFilaSelfieDi, IPooledObject
    {
        private readonly ConfiguracaoIp configuracaoIp;
        private readonly QueueClient filaComparacaoSelfieDi;

        public FilaSelfieDiCliente(ConfiguracaoIp configuracaoIp)
        {
            this.configuracaoIp = configuracaoIp;
            this.filaComparacaoSelfieDi = new QueueClient(configuracaoIp.Host, configuracaoIp.Porta);
        }

        public static FilaSelfieDiClientePool Pool
        {
            get;
            set;
        }

        public int ObterLoteId()
        {
            this.filaComparacaoSelfieDi.Connect(this.configuracaoIp.Host, this.configuracaoIp.Porta);
            return this.filaComparacaoSelfieDi.Get("faceselfie");
        }

        public void Release()
        {
            this.filaComparacaoSelfieDi.Dispose();
        }
    }
}
