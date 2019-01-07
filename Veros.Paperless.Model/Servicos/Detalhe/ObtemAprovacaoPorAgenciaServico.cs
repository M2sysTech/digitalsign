namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Aprovacao;
    using Veros.Paperless.Model.Repositorios;

    public class ObtemAprovacaoPorAgenciaServico : IObtemAprovacaoPorAgenciaServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IObtemAprovacaoServico obtemAprovacaoServico;
        private readonly IValidaSeProcessoDisponivelParaAprovacaoServico validaSeProcessoDisponivel;

        public ObtemAprovacaoPorAgenciaServico(
            IProcessoRepositorio processoRepositorio,
            IObtemAprovacaoServico obtemAprovacaoServico,
            IValidaSeProcessoDisponivelParaAprovacaoServico validaSeProcessoDisponivel)
        {
            this.processoRepositorio = processoRepositorio;
            this.obtemAprovacaoServico = obtemAprovacaoServico;
            this.validaSeProcessoDisponivel = validaSeProcessoDisponivel;
        }

        public Aprovacao Obter(string agencia)
        {
            var processo = this.processoRepositorio.ObterPorAgenciaParaAprovacao(agencia);

            if (processo == null)
            {
                return null;
            }

            return this.obtemAprovacaoServico.Obter(processo);
        }
    }
}