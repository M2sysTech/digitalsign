namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface IObtemLotesParaAmostragemQualidadeCefConsulta
    {
        IList<CodigoView> Executar(int loteCefId, int quantidade);
    }
}
