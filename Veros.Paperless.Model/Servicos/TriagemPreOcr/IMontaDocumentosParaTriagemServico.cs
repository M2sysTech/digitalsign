namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using System.Collections.Generic;
    using Entidades;
    using ViewModel;

    public interface IMontaDocumentosParaTriagemServico
    {
        IList<DocumentoParaSeparacaoViewModel> Executar(Lote lote, bool ignorarExcluidas);
    }
}
