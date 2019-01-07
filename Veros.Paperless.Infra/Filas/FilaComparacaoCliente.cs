namespace Veros.Paperless.Infra.Filas
{
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Entidades;
    using Veros.Queues.Client;

    public class FilaComparacaoCliente : IFilaComparacao, IPooledObject
    {
        private readonly ConfiguracaoIp configuracaoIp;
        private readonly QueueClient filaComparacaoFace;

        public FilaComparacaoCliente(ConfiguracaoIp configuracaoIp)
        {
            this.configuracaoIp = configuracaoIp;
            this.filaComparacaoFace = new QueueClient(configuracaoIp.Host, configuracaoIp.Porta);
        }

        public static FilaComparacaoClientePool Pool
        {
            get;
            set;
        }

        public int ObterFaceId()
        {
            this.filaComparacaoFace.Connect(this.configuracaoIp.Host, this.configuracaoIp.Porta);
            return this.filaComparacaoFace.Get("face");
        }

        public void Release()
        {
            this.filaComparacaoFace.Dispose();
        }
    }
}
