namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using System.Collections.Generic;
    using ViewModel;

    public interface IExecutaAcoesTriagemServico
    {
        LoteTriagemViewModel ExecutarAcoes(int processoId, string textoDeAcoes, bool ignorarPaginasExcluidas, bool excluirDocumentosSemPaginas, string fase);
    }
}
