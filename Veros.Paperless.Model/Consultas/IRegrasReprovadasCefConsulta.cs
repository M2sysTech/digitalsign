namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;
    using ViewModel;

    public interface IRegrasReprovadasCefConsulta
    {
        IList<RegraVioladaReprovacaoCefViewModel> Obter(int lotecefId);
    }
}