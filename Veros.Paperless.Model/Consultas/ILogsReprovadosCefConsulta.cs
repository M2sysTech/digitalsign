namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;
    using ViewModel;

    public interface ILogsReprovadosCefConsulta
    {
        IList<LogDeReprovacaoCefViewModel> Obter(int lotecefId);
    }
}