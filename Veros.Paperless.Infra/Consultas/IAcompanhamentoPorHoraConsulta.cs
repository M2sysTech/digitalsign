namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;

    public interface IAcompanhamentoPorHoraConsulta
    {
        IList<Rajada> Obter(int pacoteProcessadoId);
    }
}