namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface ILoteCefProntoParaFecharConsulta
    {
        IList<LotecefPronto> ObterQuantidades(int lotecefId);
    }
}