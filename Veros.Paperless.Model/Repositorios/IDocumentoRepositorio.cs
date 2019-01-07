namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Consultas;
    using Entidades;
    using Framework.Modelo;

    public interface IDocumentoRepositorio : IRepositorio<Documento>
    {
        IList<Documento> ObterTodosPorLoteComPaginasEIndexacao(int loteId);

        Documento ObterPacDoLote(Lote lote);

        void AtualizaStatusDocumento(int documentoId, DocumentoStatus status);

        IList<Documento> ObterTodosPorProcesso(Processo processo);

        IList<TotalDocumentoPorFaseConsulta> ObterTotalDeDocumentosPorFase();

        void ConcluirMontagemDocumento(int documentoId, string templates);

        IList<Documento> ObterDocumentosPendentesDeMontagem();

        void AlterarIndicioDeFraude(int documentoId, string indicioDeFraude);

        IList<Documento> ObterDocumentosPorParticipante(int processoId, string participante, string cpfParticipante);

        IList<Documento> ObterPendentesDeConsulta();

        void AlterarStatusDeConsulta(int documentoId, DocumentoStatus statusDeConsulta);

        void AlterarStatusPorLote(int loteId, DocumentoStatus documentoStatus);        

        bool TemNaoIdentificadosPorLote(Lote lote);
        
        void LimparHoraInicio(int documentoId);

        void LimparHoraInicioEResponsavel(int documentoId);

        void Reclassificar(int documentoId, int tipoDocumentoId, DocumentoStatus status);
        
        IList<Documento> ObterDocumentosDoLotePorTipo(int loteId, int tipoDocumentoId);

        IList<Documento> ObterDocumentosDoProcessoComCpf(int processoId, string cpf);

        IList<Documento> ObterPorProcessoEParticipante(int processoId, string cpfDoParticipante, string sequencialDoParticipante);

        void AlterarTipo(int documentoId, int tipoDocumentoId);
        
        void GravarComoFichaVirtual(int documentoId);

        IList<Documento> ObterComPaginasPendentesClassifier();

        IList<Documento> ObterTodosPorLote(int loteId);

        void AlterarStatus(int documentoId, DocumentoStatus documentoStatus);

        IList<int> ObterListaDePendentesDeAntiFraude();

        IList<int> ObterListaDePendentesDeAjuste();

        Documento ObterParaAntiFraude(int documentoId);

        void AlterarStatusFraude(int documentoId, string status);

        IList<Documento> ObterPorProcessoETipo(int processoId, int tipoDoc);
        
        Documento ObterComPaginas(int documentoId);

        IList<Documento> ObterComPaginas(Lote lote);

        void AlterarTemplate(Lote lote, int tipoDocumentoId, string template);

        IList<Documento> ObterParaClassificacao(int usuarioResponsavelId);

        IList<Documento> ObterTodosPorLoteComTipoDocumento(int loteId);

        Documento ObterComPacote(int documentoId);

        Documento ObterComPacoteProcessado(int documentoId);

        IList<Documento> ObterComTipoPorLote(int loteId);

        bool TodasOsPdfsEstaoAssinados(int loteId);

        Documento ObterDocumentoPaiComPaginas(int documentoId);

        IList<Documento> ObterPendentesDeRecognitionServer();

        IList<Documento> ObterProcessadosPeloRecognitionServer();

        void AtualizarResponsavel(int documentoId, int usuarioId);

        int ObterQuantidadePorLote(int loteId);

        IList<Documento> ObterPreparacaoParaAjustePorLote(int loteId);

        void AtualizarStatusPorProcesso(int processoId, DocumentoStatus statusAtual, DocumentoStatus novoStatus);

        IList<int> ObterListaIdsEmPreparacaoParaAjuste();

        IList<Documento> ObterDocumentosEmPreparacaoParaAjuste(int documentoId);

        IList<Documento> ObterPendentesDeRecognitionServerPosAjuste();

        IList<Documento> ObterProcessadosPeloRecognitionServerPosAjuste();

        void AlterarTipoOriginal(Documento documento, TipoDocumento tipoDocumentoNovo);

        Documento ObterFolhaDeRosto(int processoId);

        Documento ObterTermoAutuacao(int processoId);

        IList<Documento> ObterPdfsPorProcesso(int processoId);

        void AtualizarQuantidadeDePaginas(int id, int totalPaginasDoDocumento);

        IList<Documento> ObterTermos(int processoId);

        void AlterarOrdem(int documentoId, int novaOrdem);

        void AlterarMarca(int documentoId, string marca);

        void MarcarDocumentoRecontado(Documento documento, int totalPaginas);

        Documento ObterDocumentacaoGeral(int loteId);

        Documento ObterPdfFilho(Documento documentoPai);

        void AlterarOrdemPais(int documentoPaiId, int novaOrdem);

        IList<Documento> ObterPendentesDeRecognitionServerAjusteDeBrancos();

        IList<Documento> ObterProcessadosPeloRecognitionServerAjusteBrancos();

        IList<Documento> ObterFilhos(int loteId, int documentoPaiId);

        void LimparFraudes(int loteId);

        Documento ObterPdfFilhoExcluido(Documento documento);

        IList<Documento> ObterPorProcesso(int processoId);

        void SetarExcluidoAjusteTemporario(int loteId);

        void MarcarConcluidoRecognitionService(int documentoId);

        void MarcarInicioRecognitionService(int documentoId);

        void AlterarRecognitionService(int documentoId, DocumentoStatus novoStatus, bool recognitionService);

        void AlterarStatus(int processoId, int tipoDocumentoId, DocumentoStatus novoStatus);

        void MarcarInicioPosRecognitionService(int documentoId);

        void MarcarConcluidoRecognitionPosAjusteService(int documentoId);

        void ApagarPorLote(Lote lote);

        void AtualizarAposClassificacao(DocumentoStatus status, TipoDocumento tipoDocumento, int id);

        void AtualizarAposIdentificacaoManual(TipoDocumento tipoDocumento, int id);
        
        IList<Documento> ObterPorLoteComTipo(Lote lote);

        void LimparHoraInicioEResponsavel(Lote lote);

        IList<Documento> ObterDocumentosComErroDeAssinatura();

        void AjusteFinalizado(int id);

        void ExcluirCapaTermoAutenticacaoPorLote(int loteId);

        void ApagarVirtualPorLote(int loteId);
    }
}
