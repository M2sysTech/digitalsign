namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface IDevolucaoConsulta
    {
        IList<Devolucao> Obter(string dataInicio, string dataFim);
    }
}