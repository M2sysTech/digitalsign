namespace Veros.Paperless.Model.Servicos.Separacao
{
    using System.Collections.Generic;
    using Entidades;
    using ViewModel;

    public interface IMontaDocumentosParaSeparacaoServico
    {
        IList<DocumentoParaSeparacaoViewModel> Executar(Lote lote);
    }
}
