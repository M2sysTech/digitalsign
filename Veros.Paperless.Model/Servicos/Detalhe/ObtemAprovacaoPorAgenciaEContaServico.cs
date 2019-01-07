namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Aprovacao;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class ObtemAprovacaoPorAgenciaEContaServico : IObtemAprovacaoPorAgenciaEContaServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IObtemAprovacaoServico obtemAprovacaoServico;
        private readonly IValidaSeProcessoDisponivelParaAprovacaoServico validaSeProcessoDisponivel;
        private readonly ISessaoDoUsuario userSession;

        public ObtemAprovacaoPorAgenciaEContaServico(
            IProcessoRepositorio processoRepositorio,
            IObtemAprovacaoServico obtemAprovacaoServico,
            IValidaSeProcessoDisponivelParaAprovacaoServico validaSeProcessoDisponivel, 
            ISessaoDoUsuario userSession)
        {
            this.processoRepositorio = processoRepositorio;
            this.obtemAprovacaoServico = obtemAprovacaoServico;
            this.validaSeProcessoDisponivel = validaSeProcessoDisponivel;
            this.userSession = userSession;
        }

        public Aprovacao Obter(
            string agencia,
            string conta)
        {
            var processo = this.processoRepositorio.ObterPorAgenciaEContaParaAprovacao(agencia, conta);

            if (processo == null)
            {
                return null;
            }

            if (this.validaSeProcessoDisponivel.Validar(processo) == false)
            {
                return new Aprovacao
                {
                    SendoTratadaPorOutroUsuario = true,
                    Processo = processo
                };
                ////throw new RegraDeNegocioException("A conta informada está sendo analisada por outro funcionário.");
            }

            return this.obtemAprovacaoServico.Obter(processo);
        }

        public Aprovacao ObterForcado(
            string agencia,
            string conta)
        {
            var processo = this.processoRepositorio.ObterPorAgenciaEContaParaAprovacao(agencia, conta);

            if (processo == null)
            {
                return null;
            }

            this.processoRepositorio.AlterarResponsavel(processo.Id, this.userSession.UsuarioAtual.Id);

            if (this.validaSeProcessoDisponivel.ValidarStatus(processo) == false)
            {
                return new Aprovacao
                {
                    SendoTratadaPorOutroUsuario = true,
                    Processo = processo
                };
            }

            return this.obtemAprovacaoServico.Obter(processo);
        }
    }
}