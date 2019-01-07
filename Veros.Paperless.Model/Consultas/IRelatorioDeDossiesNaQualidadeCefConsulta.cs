namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IRelatorioDeDossiesNaQualidadeCefConsulta
    {
        IList<DossiesNaQualidadeCef> Obter(DateTime dataMovimento);
    }
}
