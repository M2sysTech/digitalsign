namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using Entidades;
    using Repositorios;

    public class GravaAjustesDoProcessoServico : IGravaAjustesDoProcessoServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IGravaLogDoProcessoServico gravaLogDoProcessoServico;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public GravaAjustesDoProcessoServico(
            IProcessoRepositorio processoRepositorio, 
            ILoteRepositorio loteRepositorio, 
            IGravaLogDoProcessoServico gravaLogDoProcessoServico, 
            IDocumentoRepositorio documentoRepositorio)
        {
            this.processoRepositorio = processoRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.gravaLogDoProcessoServico = gravaLogDoProcessoServico;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Executar(int processoId, string operacao, string observacao)
        {
            var processo = this.processoRepositorio.ObterPorId(processoId);

            this.documentoRepositorio.LimparFraudes(processo.Lote.Id);

            switch (operacao)
            {
                case "A":
                    this.gravaLogDoProcessoServico.Executar(LogProcesso.AcaoAjusteFinalizado, processo, observacao);
                    
                    this.documentoRepositorio.AtualizarStatusPorProcesso(
                        processoId,
                        DocumentoStatus.TelaAjuste,
                        DocumentoStatus.TelaAjusteFinalizado);
                    break;

                case "R":
                    this.loteRepositorio.AlterarStatus(processo.Lote.Id, LoteStatus.AguardandoRecaptura);
                    this.loteRepositorio.MarcarParaRecaptura(processo.Lote.Id);

                    this.processoRepositorio.AlterarStatus(processoId, ProcessoStatus.AguardandoRecaptura);
                    this.gravaLogDoProcessoServico.Executar(LogProcesso.AcaoSolicitaRecaptura, processo, observacao);
                    
                    this.documentoRepositorio.AtualizarStatusPorProcesso(
                        processoId,
                        DocumentoStatus.TelaAjuste,
                        DocumentoStatus.TelaAjusteSolicitadoRecaptura);
                    break;

                case "S":
                    this.gravaLogDoProcessoServico.Executar(LogProcesso.AcaoSolicitaSeparacaoManual, processo, observacao);

                    this.documentoRepositorio.AtualizarStatusPorProcesso(
                        processoId,
                        DocumentoStatus.TelaAjuste,
                        DocumentoStatus.TelaAjusteSolicitadoSeparacaoManual);
                    break;
            }

            foreach (var documento in processo.Documentos)
            {
                if (documento.HoraInicio != null)
                {
                    documento.HoraInicio = null;
                    documento.UsuarioResponsavelId = 0;
                }
            }

            processo.HoraInicio = null;
            processo.HoraInicioAjuste = null;
            this.processoRepositorio.Salvar(processo);
        }
    }
}
