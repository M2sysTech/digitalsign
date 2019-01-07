namespace Veros.Paperless.Model.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Consultas;
    using Entidades;
    using Framework.Modelo;
    using ViewModel;

    public interface ILoteRepositorio : IRepositorio<Lote>
    {
        IList<Lote> ObterTodosPorData(DateTime data);

        void AlterarStatusParaOcrFinalizado(int loteId);

        IList<TotalDocumentoPorFaseConsulta> ObterTotalDeDocumentosPorFase();

        IList<Lote> ObterPorPacote(PacoteProcessado pacoteProcessado);

        IList<Lote> ObterPorPacoteId(int pacoteProcessadoId);

        IList<Lote> ObterPorDataDeCadastro(DateTime dataInicio, DateTime dataFim);

        Lote ObterLoteParaProcessarNoWorkflow(int loteId);

        void AlterarStatus(int loteId, LoteStatus status);

        IList<int> ObterIdsParaProcessarNoWorkflow();

        IList<Lote> ObterPendentesDeRetransmissao();

        void GravarHoraGeracaoArquivoXml(int loteId);

        IList<Lote> ObterLotesParaExpurgo(int quantidadeDeDias);
        
        Lote ObterPorIdentificacao(string identificacao);

        IList<string> ObterIdentificacoesParaExpurgo(int intervaloDeDias);
        
        Lote ObterUltimo();

        IList<int> ObterIdsComStatus(LoteStatus status);

        IList<Lote> ObterPendentesDeImportacao();

        IList<Lote> ObterPendentesConsultaVertros();

        IList<Lote> ObterPendentesComplementacao();

        void AlterarParaCapturaFinalizada(int loteId, PacoteProcessado pacoteProcessado);

        void AtualizaParaLoteConsultado(int id);
        
        void AtualizaVertrosOk(int id);

        IList<Lote> ObterComPaginasPendentesClassifier();

        IEnumerable<Lote> ObterUltimos();

        void AlterarParaRecepcaoFinalizada(int loteId);

        IList<Lote> ObterPendentesDeCaptura();

        IList<Lote> ObterPorDossieId(int dossieId);

        void GravarResultadoQualidadeCef(int id, LoteStatus novoStatus, string resultadoAnalise);

        void AlterarParaEncerrarDigitalizacao(int loteId);

        Lote ObterPorProcessoId(int processoId);

        IList<int> ObterPendentesGeracaoTermosAvulsos();

        void MarcarTermoAvulsoRealizado(int loteId);

        void EnviarParaQualidadeCef(int loteId, int novaSituacaoAmostragem);

        void MarcarAvaliacaoBrancoOk(int loteId);

        IList<int> ObterLotesParaReprocessamentoPrioritariosBrancos();

        void AlterarParaRecapturaFinalizada(int loteId);

        void MarcarParaRecaptura(int loteId);

        void SetarExcluido(int loteId, string motivo);

        Lote ObterComPacote(int loteId);

        void SetarMarcaQualidade(int loteId, string loteMarcaQualidade);

        void SetarParaRecaptura(int loteId, string motivo);

        IList<Lote> ObterPorLoteCefId(int lotecefId);

        IList<Lote> ObterParaCertificadoQualidade(int lotecefId);

        void SetarPriorizacaoDeLote(int loteId, string resultadoQualidadeCef);        

        IList<Lote> ObterLotesAbertosComQualiM2Sys();

        void EnviarParaQualidadeMudandoLote(int loteId, int lotecefId);

        void AtualizarLotecef(int loteId, int lotecefId);

        void PriorizarLote(int loteId);

        IList<Lote> ObterPorLoteCef(int loteCefId);

        void AtualizarStatus(IEnumerable<Lote> lotes, LoteStatus novoStatus, int identificacaoNovaAmostra);
        
        void EnviarParaQualidadeM2(List<Lote> lotes);
        
        Lote ObterMaiorAmostraPorLoteCef(int loteCefId);

        IList<int> ObterPendentesEnvioCloud();

        IList<int> ObterJpgsPendentesEnvioCloud();

        void MarcarComoEnviadoParaCloud(int loteId);

        void MarcarComoJpegsEnviadosParaCloud(int loteId);

        IList<int> ObterPendentesExpurgoFileTransfer();

        void MarcarComoRemovidoFileTransferM2(int loteId);

        void MarcarParaEnviarParaFileTransfer(Lote lote);

        void AtualizarTodosParaAprovadoPorLotecef(int loteCefId);

        void AtualizarTodosParaFaturaEmitidaPorLotecef(int lotecefId);
    }
}
