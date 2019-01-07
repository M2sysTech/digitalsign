namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using Entidades;
    using Repositorios;
    using ViewModel;

    public class SalvaTriagemPreOcrServico : ISalvaTriagemPreOcrServico
    {
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IExecutaAcoesTriagemServico executaAcoesTriagemServico;

        public SalvaTriagemPreOcrServico(
            IGravaLogDoLoteServico gravaLogDoLoteServico,
            IProcessoRepositorio processoRepositorio, 
            ILoteRepositorio loteRepositorio, IExecutaAcoesTriagemServico executaAcoesTriagemServico)
        {
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.processoRepositorio = processoRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.executaAcoesTriagemServico = executaAcoesTriagemServico;
        }
        
        public void Executar(int processoId)
        {
            var processo = this.processoRepositorio.ObterPorId(processoId);
            this.gravaLogDoLoteServico.Executar(LogLote.AcaoTriagemRealizada, processo.Lote.Id, "Lote foi salvo na triagem sem alterações");
            this.FinalizarTriagem(processo.Lote.Id, processoId);
        }

        public void Executar(int loteId, int processoId, string textoDeAcoes)
        {
            this.executaAcoesTriagemServico.ExecutarAcoes(processoId, textoDeAcoes, true, true, LoteTriagemViewModel.FaseTriagem);
            this.gravaLogDoLoteServico.Executar(LogLote.AcaoTriagemRealizada, loteId, "Lote foi salvo na triagem");
            this.FinalizarTriagem(loteId, processoId);
        }

        public void ExecutarExclusoes(int loteId, int processoId, string textoDeAcoes)
        {
            this.executaAcoesTriagemServico.ExecutarAcoes(processoId, textoDeAcoes, false, false, LoteTriagemViewModel.FaseTriagem);
            this.gravaLogDoLoteServico.Executar(LogLote.AcaoTratamentoDeExcluidasDaTriagem, loteId, "Tratamento de páginas excluídas foi realizado na triagem");
        }

        private void FinalizarTriagem(int loteId, int processoId)
        {
            this.processoRepositorio.AlterarStatus(processoId, ProcessoStatus.AguardandoTransmissao, ProcessoStatus.AguardandoTriagem);
            this.processoRepositorio.LimparResponsavelEHoraInicio(processoId, ProcessoStatus.AguardandoTriagem);
            this.loteRepositorio.AlterarStatus(loteId, LoteStatus.AguardandoIdentificacaoManual);
            ////todo: qual status os documentos e as páginas devem ficar?
        }
    }
}
