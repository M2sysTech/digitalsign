namespace Veros.Paperless.Infra.Filas
{
    using Framework.Servicos;
    using Model.Entidades;
    using Queues.Client;

    public class FilaFaceExtractorCliente : IFilaFaceExtractorCliente, IPooledObject
    {
        private readonly ConfiguracaoIp configuracaoIp;
        private readonly QueueClient filaFaceExtractor;

        public FilaFaceExtractorCliente(ConfiguracaoIp configuracaoIp)
        {
            this.configuracaoIp = configuracaoIp;
            this.filaFaceExtractor = new QueueClient(configuracaoIp.Host, configuracaoIp.Porta);
        }

        public static FilaFaceExtractorClientePool Pool
        {
            get;
            set;
        }

        public int Obter()
        {
            this.filaFaceExtractor.Connect(this.configuracaoIp.Host, this.configuracaoIp.Porta);
            return this.filaFaceExtractor.Get("faceext");
        }

        public void Release()
        {
            this.filaFaceExtractor.Dispose();
        }
    }
}
