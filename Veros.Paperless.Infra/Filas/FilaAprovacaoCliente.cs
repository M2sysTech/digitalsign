namespace Veros.Paperless.Infra.Filas
{
    using Queues.Client;
    using Veros.Paperless.Model.Filas;
    using Veros.Paperless.Model.Repositorios;
    using Veros.Queues;

    public class FilaAprovacaoCliente : IFilaAprovacaoCliente
    {
        private readonly IConfiguracaoIpRepositorio configuracaoIpRepositorio;

        public FilaAprovacaoCliente(IConfiguracaoIpRepositorio configuracaoIpRepositorio)
        {
            this.configuracaoIpRepositorio = configuracaoIpRepositorio;
        }

        public int ObterProximo()
        {
            var configuracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoDaFila();

            var clienteFila = new QueueClient(
                configuracaoIp.Host,
                configuracaoIp.Porta);

            return clienteFila.Get("aprov");
        }
    }
}