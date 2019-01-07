namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IFaturamentoPorPeriodoConsulta
    {
        IList<FaturamentoPorPeriodo> Obter(DateTime dataInicial, DateTime dataFinal);
    }
}
