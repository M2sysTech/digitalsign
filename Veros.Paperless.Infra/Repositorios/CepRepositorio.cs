namespace Veros.Paperless.Infra.Repositorios
{
    public class CepRepositorio : Veros.Paperless.Model.Repositorios.ICepRepositorio
    {
        private readonly ConfiguracaoIpRepositorio configuracaoIpRepositorio;

        public CepRepositorio(ConfiguracaoIpRepositorio configuracaoIpRepositorio)
        {
            this.configuracaoIpRepositorio = configuracaoIpRepositorio;
        }

        public Veros.Cep.Endereco ObterEndereco(int cep)
        {
            var configuracaoIp = this.configuracaoIpRepositorio.ObterPorTag("CONSULTA_CEP");
            Veros.Cep.Endereco endereco;

            try
            {
                endereco = new ConsultaCep(configuracaoIp.Host, configuracaoIp.Porta).Consultar(cep);
            }
            catch (System.Exception exception)
            {
                endereco = new Endereco();
            }            

            return endereco;
        }
    }
}