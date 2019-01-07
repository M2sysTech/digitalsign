namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface IDocumentosPdfPorStatusConsulta
    {
        IList<DocumentosPdfConsulta> Obter(string statusConsulta);
    }
}
