namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;
    using Entidades;

    public interface IObtemGruposDeTiposDeDocumentosServico
    {
        IList<TipoDocumento> Obter();
    }
}