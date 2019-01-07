namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IRelatorioDeFaturamentoConsulta
    {
        IList<DossieParaFaturamento> Obter(DateTime dataInicio, DateTime dataFim);
    }
}