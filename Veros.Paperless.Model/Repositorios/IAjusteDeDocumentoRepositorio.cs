namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IAjusteDeDocumentoRepositorio : IRepositorio<AjusteDeDocumento>
    {
        IList<AjusteDeDocumento> ObterPorDocumento(int documentoId);

        IList<AjusteDeDocumento> ObterPorProcesso(int processoId);

        void GravarComoProcessado(AjusteDeDocumento ajuste);

        void AlterarStatus(int ajusteId, string situacao);

        IList<AjusteDeDocumento> ObterPendentes(int documentoId);

        IList<AjusteDeDocumento> ObterPendentesPorProcessoComPaginas(int processoId);

        void GravarTodosComoProcessado(Pagina pagina);

        IList<AjusteDeDocumento> ObterPendentes(Lote lote);

        bool PossuiRegistro(Processo processo);

        IList<AjusteDeDocumento> ObterPendentesPorPagina(int paginaId);

        void GravarComoErro(AjusteDeDocumento ajuste);
    }
}