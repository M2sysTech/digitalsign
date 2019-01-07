namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface ICaixasParaDevolucaoConsulta
    {
        IList<dynamic> Obter(DateTime? diaDevolucao);
    }
}
