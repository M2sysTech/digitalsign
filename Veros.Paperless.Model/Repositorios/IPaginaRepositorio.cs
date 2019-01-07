namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections;
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IPaginaRepositorio : IRepositorio<Pagina>
    {
        IList<Pagina> ObterPorDocumento(Documento documento);

        IList<Pagina> ObterPorDocumentoId(int documentoId);

        IEnumerable<Pagina> ObterPorLote(Lote lote);

        void AtualizarPaginaComoNaoReconhecido(int paginaId);
        
        void AtualizarPaginaComoReconhecido(int paginaId);

        void AtualizarPaginaInicioOcr(int paginaId);

        IList<Pagina> ObterPorEstatisticaFaturamento(int faturamentoId);

        IList<Pagina> ObterPorDiaProcessado(string ddmmaaaa);

        void SalvarOrdemDaPagina(int paginaId, int ordemPagina);

        IList<Pagina> ObterPorLoteParaRetransmissao(int loteId);
        
        Pagina ObterPorIdComDocumento(int paginaId);

        void AtualizarPaginaTipoDocumento(int paginaId, string tipoArquivo);

        void AtualizarTudoComoReconhecido();

        void AtualizarTipoEtamanho(int paginaId, string extensaoNovaImagem, long tamanhoImagem);

        void AtualizarPaginaFaceExtractor(int paginaId, string novoStatus);

        IList<Pagina> ObterPorStatus(PaginaStatus paginaStatus);

        Pagina ObterPdfDossier(int processoId);

        IList<Pagina> ObterPorOrdem(int documentoId, int ordem);

        Pagina ObterPdfDocumento(int documentoId);

        IList<Pagina> ObterPorProcesso(int processoId);

        void AlterarStatus(Documento documento, int ordem, PaginaStatus paginaStatus);

        void AlterarStatusPdfaProcessados(int documentoId);

        void AlterarStatusDaPagina(int paginaId, PaginaStatus statusParaReconhecimentoPosAjuste);

        IList<Pagina> ObterPaginasAjustadas(Documento documento);

        void AlterarStatusDaPaginaPorDocumento(int documentoId, PaginaStatus statusPagina);

        IList<Pagina> ObterTodosJpegsValidos(int loteId);

        void RestaurarPaginaExcluida(int paginaId, int documentoDestinoId);
        
        void AlterarStatus(int paginaId, PaginaStatus status);

        void AtualizarDataCenter(int paginaId, int dataCenterId);

        IList<Pagina> ObterTipoPng(Documento documento);

        Pagina ObterComDocumento(int paginaId);

        void ApagarPorLote(Lote lote);

        void AlterarOrdem(int paginaId, int novaOrdem);

        void AlterarDocumento(int paginaId, int documentoId);

        IList<Pagina> ObterPaginaNaoExcluida(int documentoId);

        IList<Pagina> ObterPdfsDoLote(int loteId);

        void MarcarComoEnviadaCloud(int paginaId);

        IList<Pagina> ObterJpegsDoLote(int loteId);

        void MarcarComoRemovidoFileTransferM2(int paginaId);

        void AtualizarDataCenterAntesCloud(int paginaId, int dataCenter);

        void AlterarStatusDaPaginaNaoExcluidaPorLote(int loteId, PaginaStatus paginaStatus);

        void ExcluirPaginasPorLote(int loteId);
    }
}
