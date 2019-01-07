namespace Veros.Paperless.Model.Servicos.Pacotes
{
    using System;
    using Consultas;
    using System.Collections.Generic;

    public interface IObtemRelatorioDePacotesProcessadosServico
    {
        IList<PacotesProcessadosConsulta> Obter(DateTime dataInicio, DateTime dataFim);
    }
}
