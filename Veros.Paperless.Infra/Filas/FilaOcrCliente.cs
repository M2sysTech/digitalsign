namespace Veros.Paperless.Infra.Filas
{
    using Framework;
    using Framework.Servicos;
    using Model.Entidades;
    using Queues.Client;

    public class FilaOcrCliente : IFilaOcrCliente, IPooledObject
    {
        private readonly ConfiguracaoIp configuracaoIp;
        private readonly QueueClient filaOcr;

        public FilaOcrCliente(ConfiguracaoIp configuracaoIp)
        {
            this.configuracaoIp = configuracaoIp;
            this.filaOcr = new QueueClient(configuracaoIp.Host, configuracaoIp.Porta);
        }

        public static FilaOcrClientePool Pool
        {
            get;
            set;
        }

        public int Obter()
        {
            this.filaOcr.Connect(this.configuracaoIp.Host, this.configuracaoIp.Porta);
            return this.filaOcr.Get("ocr", Aplicacao.Versao);
        }

        public void Release()
        {
            this.filaOcr.Dispose();
        }
    }
}
