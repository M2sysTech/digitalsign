namespace Veros.Paperless.Model.Repositorios
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;
    using ViewModel;

    public interface IProcessoRepositorio : IRepositorio<Processo>
    {
        IList<Processo> ObterTodosComStatusEmValidacao();

        IList<Processo> ObterPorAgenciaEConta(string agencia, string conta);

        Processo ObterPorAgenciaEContaAguardandoRetorno(string agencia, string conta);

        void DevolverProcesso(int processoId, string parecerDoBanco);

        void AlterarStatus(int id, ProcessoStatus status);

        IList<Processo> ObterTodosParaExportacao(int take = 25);
        
        void LimparResponsavelEHoraInicio(int processoId);

        void AlterarResponsavel(int processoId, int usuarioId);

        Processo ObterPorAgenciaEContaParaAprovacao(string agencia, string conta);
        
        bool ExisteDocumentoComFraude(int processoId);

        IList<Processo> ObterPorPacoteProcessado(int pacoteId);

        IList<Processo> ObterPorPacoteProcessado(int pacoteId, int tipoProcessoId);

        Processo ObterPorAgenciaParaAprovacao(string agencia);

        IList<int> ObterIdsParaProcessarNoWorkflow();

        IList<Processo> ObterPorLote(Lote lote);

        void AlterarStatusPorLote(int loteId, ProcessoStatus processoStatus);

        void AlterarStatusPorLote(List<Lote> lotes, ProcessoStatus processoStatus);

        IList<Processo> ObterPorPacoteComStatusFinalizado(int id);
        
        int ObterTotaldeLotesFinalizadosPorPacoteProcessado(int id);

        int ObterTotaldeLotesFinalizadosEComErroPorPacoteProcessado(int id);

        bool EstaAguardandoAprovacao(int processoId);
        
        void AtualizarHoraInicio(int processoId);

        Processo ObterPorIdParaExportacao(int id);

        int AlterarStatusAposRetornoPorAgenciaConta(string agencia, string conta, ProcessoStatus processoStatus);
        
        Processo ObterProcessoParaProcessarNoWorkFlow(int processoId);

        Processo ObterComPacoteProcessado(int id);

        void AlterarDecisao(int id, ProcessoDecisao processoDecisao);

        IList<int> ObterIdsDosProcessosParaExpurgar();
        
        IList<int> ObterIdsDosProcessosParaMontagem();
        
        Processo ObterProcessoParaProcessarNaMontagem(int processoId);

        IList<Processo> ObterPorPacoteProcessadoSemPaginas(PacoteProcessado pacoteProcessado);
        
        Documento ObterDocumentoPac(int processoId);
        
        void GravarAprovacao(int processoId);

        Lote ObterLotePorProcessoId(int processoId);

        Processo ObterPorAgenciaContaEPacote(string agencia, string numeroDaConta, int pacoteProcessadoId);

        Processo ObterProcessoFinalizadoOuErroTerceira(string agencia, string conta);
        
        Processo ObterPorIdentificacao(string identificacao);

        void GravarFinalizado(int processoId, ProcessoStatus status);

        Processo ObterUltimoPorIdentificacao(string identificacao);
        
        Processo ObterProcessoParaExportacao(int processoId);

        IList<int> ObterIdsParaExportacao();

        void MarcarProcessoComoExportado(int processoId);
        
        Processo ObterPorLoteId(int loteId);

        IList<Processo> ObterProcessosParaClassifier();

        IList<Processo> ObterProcessosParaClassifierPorProcessoId(int processoId);

        Processo ObterProcessoPorIdComPaginas(int processoId);
        
        void SalvarProcessamentoDoClassifier(int processoId, int acaoClassifier);

        IList<Processo> Pesquisar(PesquisaDossieViewModel filtros);
        
        Processo ObterDetalheDossie(int processoId);
        
        Processo ObterDetalheDossieComDocumentos(int processoId);

        Processo ObterPorLote(int loteId);

        IList<Processo> ObterTodosDoPacote(Pacote pacote);

        IList<Processo> ObterRelatorioDeDevolucao(DateTime? dataInicio, DateTime? dataFim);                

        IList<Processo> ObterPorPeriodoDeColeta(DateTime? dataInicio, DateTime? dataFim);

        IList<int> ObterProcessosParaSeparacao();

        IList<Processo> ObterTodosPorPeriodoDeColeta(DateTime? dataInicio, DateTime? dataFim);

        IList<Processo> ObterParaControleQualidade(int usuarioResponsavelId, ProcessoStatus status);

        IList<Processo> ObterParaAjustes(int usuarioResponsavelId);

        Processo ObterComPacote(int processsoId);

        void AtualizarQuantidadeDePaginas(int processoId, int count);
        
        Processo ObterPorIdComTipoDeProcesso(int processoId);
        
        IList<Processo> ObterPendentesDeRecaptura(string caixa, string barcode);
        
        void AlterarStatus(int processoId, ProcessoStatus novoStatus, ProcessoStatus statusAtual);

        IList<Processo> ObterPorLoteComDocumentos(int loteId);
        
        void AlterarTipoDossie(int processoId, string tipoDossie);

        void AlterarIdentificacao(int processoId, string identificacao, string barcode);

        void AlterarDossie(int processoId, int tipoProcessoId, string identificacao);
        
        void EnviarParaQualidadeCef(int loteId);

        IList<Processo> ObterPendentesDeCaptura(string caixa, string identificacao);

        Processo ObterComLote(int processoId);

        void LimparResponsavelEHoraInicio(int processoId, ProcessoStatus processoStatus);

        IList<Processo> ObterPorCaixa(int caixaId);

        IList<Processo> ObterPorPacote(int pacoteId);

        void SetarRetirarDaFila(int loteId, DateTime? data, int resultadoFila);
    }
}
