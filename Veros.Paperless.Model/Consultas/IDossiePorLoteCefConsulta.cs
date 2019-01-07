namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;
    using ViewModel;

    public interface IDossiePorLoteCefConsulta
    {
        IList<DossieStatusViewModel> Obter(int lotecefId);
    }
}