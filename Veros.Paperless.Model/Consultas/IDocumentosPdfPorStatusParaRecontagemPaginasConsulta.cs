namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface IDocumentosPdfPorStatusParaRecontagemPaginasConsulta
    {
        IList<DocumentoRecontar> Obter(string statusConsulta);
    }
}