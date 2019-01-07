namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface ITotaisPorPacoteProcessadoConsulta
    {
        IList<TotaisPorPacoteProcessado> Obter();
    }
}
