namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;
    using ViewModel;

    public interface IDossieStatusConsulta
    {
        IList<DossieStatusViewModel> ObterFracionado(int contador, int jumps, DateTime? dataLoteCef, int coletaId = 0);
    }
}