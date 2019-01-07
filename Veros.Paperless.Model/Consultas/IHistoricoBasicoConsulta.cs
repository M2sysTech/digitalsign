namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;
    using Entidades;
    using ViewModel;

    public interface IHistoricoBasicoConsulta
    {
        IList<HistoricoBasicoViewModel> Obter(Lote lote);
    }
}
