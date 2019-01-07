namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IObtemPaginasParaExpurgoConsulta
    {
        IList<PaginaExpurgoConsulta> Obter(DateTime? dataLimite);
    }
}
