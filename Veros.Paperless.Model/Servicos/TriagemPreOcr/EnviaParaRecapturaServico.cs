namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using Entidades;
    using Repositorios;

    public class EnviaParaRecapturaServico : IEnviaParaRecapturaServico
    {
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoProcessoServico gravaLogDoProcessoServico;

        public EnviaParaRecapturaServico(IProcessoRepositorio processoRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            ILoteRepositorio loteRepositorio, 
            IGravaLogDoProcessoServico gravaLogDoProcessoServico)
        {
            this.processoRepositorio = processoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.gravaLogDoProcessoServico = gravaLogDoProcessoServico;
        }

        public void Executar(int processoId, string observacao, string fase)
        {
            var processo = this.processoRepositorio.ObterPorId(processoId);

            this.documentoRepositorio.LimparFraudes(processo.Lote.Id);

            this.loteRepositorio.AlterarStatus(processo.Lote.Id, LoteStatus.AguardandoRecaptura);
            this.loteRepositorio.MarcarParaRecaptura(processo.Lote.Id);

            this.processoRepositorio.AlterarStatus(processoId, ProcessoStatus.AguardandoRecaptura);
            this.gravaLogDoProcessoServico.Executar(LogProcesso.AcaoSolicitaRecaptura, processo, fase + " - Enviado para recaptura: " + observacao);

            this.documentoRepositorio.AtualizarStatusPorProcesso(
                processoId,
                DocumentoStatus.TelaAjuste,
                DocumentoStatus.TelaAjusteSolicitadoRecaptura);

            this.documentoRepositorio.LimparHoraInicioEResponsavel(processo.Lote);

            this.processoRepositorio.Salvar(processo);
        }
    }
}
