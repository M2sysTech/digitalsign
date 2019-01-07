namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;
    using ViewModel;

    public interface ILogsAjusteConsulta
    {
        IList<LogDeAjusteViewModel> Obter(int loteId);
    }
}