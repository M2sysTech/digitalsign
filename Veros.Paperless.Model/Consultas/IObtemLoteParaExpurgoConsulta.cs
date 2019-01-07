namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface IObtemLoteParaExpurgoConsulta
    {
        IList<LoteParaExpurgoView> ObterDaBk(int pacoteId);

        IList<LoteParaExpurgoView> ObterDaHist(int pacoteId);
    }
}