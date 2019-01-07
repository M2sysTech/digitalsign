namespace Veros.Paperless.Model.Servicos.Aprovacao
{
    using Entidades;
    using Repositorios;
    using Filas;

    public class ObtemProcessoPorFilaDeAprovacaoEspecialServico : IObtemProcessoPorFilaDeAprovacaoEspecialServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IFilaAprovacaoEspecialCliente filaAprovacaoEspecialCliente;
        private readonly IValidaSeProcessoDisponivelParaAprovacaoServico validaSeProcessoDisponivel;

        public ObtemProcessoPorFilaDeAprovacaoEspecialServico(
            IProcessoRepositorio processoRepositorio,
            IFilaAprovacaoEspecialCliente filaAprovacaoEspecialCliente,
            IValidaSeProcessoDisponivelParaAprovacaoServico validaSeProcessoDisponivel)
        {
            this.processoRepositorio = processoRepositorio;
            this.filaAprovacaoEspecialCliente = filaAprovacaoEspecialCliente;
            this.validaSeProcessoDisponivel = validaSeProcessoDisponivel;
        }

        public Processo Obter()
        {
            while (true)
            {
                var processoId = this.filaAprovacaoEspecialCliente.ObterProximo();

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