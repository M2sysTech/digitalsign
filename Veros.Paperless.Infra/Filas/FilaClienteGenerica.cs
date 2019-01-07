namespace Veros.Paperless.Infra.Filas
{
    using Queues.Client;
    using Veros.Paperless.Model.Filas;
    using Veros.Paperless.Model.Repositorios;

    public class FilaClienteGenerica : IFilaClienteGenerica
    {
        private readonly IConfiguracaoIpRepositorio configuracaoIpRepositorio;

        public FilaClienteGenerica(IConfiguracaoIpRepositorio configuracaoIpRepositorio)
        {
            this.configuracaoIpRepositorio = configuracaoIpRepositorio;
        }
        
        public int ObterProximo(string nomeDaFila)
        {
            var configuracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoDaFila();

            var clienteFila = new QueueClient(
                configuracaoIp.Host,
                configuracaoIp.Porta);

            return clienteFila.Get(nomeDaFila);
        }

        public int ObterProximo(string nomeDaFila, int parametro)
        {
            return this.ObterProximo(nomeDaFila, parametro.ToString());
        }

        public int ObterProximo(string nomeDaFila, string parametro)
        {
            var configuracaoIp = this.configuracaoIpRepositorio.ObterConfiguracaoDaFila();

            var clienteFila = new QueueClient(
                configuracaoIp.Host,
                configuracaoIp.Porta);

            return clienteFila.Get(nomeDaFila, parametro);
        }
    }
}