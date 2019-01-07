namespace Veros.Paperless.Model.Servicos.Monitor
{
    using System.Collections.Generic;
    using Consultas;

    public interface ITotalDocumentosPorFaseServico
    {
        IList<TotalDocumentoPorFaseConsulta> Executar();
    }
}