namespace Veros.Paperless.Model.Servicos.Captura
{
    using Repositorios;

    public class FinalizaCapturaServico : IFinalizaCapturaServico
    {
        private readonly IPacoteProcessadoFactory pacoteProcessadoFactory;
        private readonly IPacoteProcessadoRepositorio pacoteProcessadoRepositorio;
        private readonly ILoteRepositorio loteRepositorio;

        public FinalizaCapturaServico(IPacoteProcessadoFactory pacoteProcessadoFactory, 
            IPacoteProcessadoRepositorio pacoteProcessadoRepositorio,
            ILoteRepositorio loteRepositorio)
        {
            this.pacoteProcessadoFactory = pacoteProcessadoFactory;
            this.pacoteProcessadoRepositorio = pacoteProcessadoRepositorio;
            this.loteRepositorio = loteRepositorio;
        }

        public void Executar(int loteId)
        {
            var lote = this.loteRepositorio.ObterPorId(loteId);

            if (lote.Recapturado)
            {
                this.loteRepositorio.AlterarParaRecapturaFinalizada(loteId);
                return;
            }
            
            var pacoteProcessado = this.pacoteProcessadoFactory.ObterDoDia();

            if (pacoteProcessado.Id < 1)
            {
                this.pacoteProcessadoRepositorio.Salvar(pacoteProcessado);
            }

            this.loteRepositorio.AlterarParaCapturaFinalizada(loteId, pacoteProcessado);
        }
    }
}
