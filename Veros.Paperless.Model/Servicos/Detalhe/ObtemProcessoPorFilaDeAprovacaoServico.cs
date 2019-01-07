namespace Veros.Paperless.Model.Servicos.Aprovacao
{
    using Entidades;
    using Repositorios;
    using Filas;

    public class ObtemProcessoPorFilaDeAprovacaoServico : IObtemProcessoPorFilaDeAprovacaoServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IFilaAprovacaoCliente filaAprovacaoCliente;
        private readonly IValidaSeProcessoDisponivelParaAprovacaoServico validaSeProcessoDisponivel;

        public ObtemProcessoPorFilaDeAprovacaoServico(
            IProcessoRepositorio processoRepositorio,
            IFilaAprovacaoCliente filaAprovacaoCliente,
            IValidaSeProcessoDisponivelParaAprovacaoServico validaSeProcessoDisponivel)
        {
            this.processoRepositorio = processoRepositorio;
            this.filaAprovacaoCliente = filaAprovacaoCliente;
            this.validaSeProcessoDisponivel = validaSeProcessoDisponivel;
        }

        public Processo Obter()
        {
            while (true)
            {
                var processoId = this.filaAprovacaoCliente.ObterProximo();

                if (processoId == 0)
                {
                    return null;
                }

                var processo = this.processoRepositorio.ObterPorId(processoId);

                if (this.validaSeProcessoDisponivel.Validar(processo))
                {
                    return processo;
                }
            }
        }
    }
}