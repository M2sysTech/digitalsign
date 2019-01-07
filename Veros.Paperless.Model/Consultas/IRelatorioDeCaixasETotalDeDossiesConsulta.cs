namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface IRelatorioDeCaixasETotalDeDossiesConsulta
    {
        IList<CaixaETotalDeDossies> Obter(string dataInicio, string dataFim);
    }
}
