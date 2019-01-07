namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;
    using ViewModel;

    public interface IProdutividadeOperadorConsulta
    {
        IList<ProdutividadeOperadorViewModel> Obter(string dataInicio, string dataFim);
    }
}