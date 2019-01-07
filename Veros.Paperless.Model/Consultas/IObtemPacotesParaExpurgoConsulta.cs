namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface IObtemPacotesParaExpurgoConsulta
    {
        IList<PacoteParaExpurgoView> ObterDaBk(int quantidadeDeDias);

        IList<PacoteParaExpurgoView> ObterDaHist(int quantidadeDeDias);
    }
}