namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IPainelDeAcompanhamentoColetaConsulta
    {
        IList<PainelDeAcompanhamentoColeta> Obter(DateTime? dataInicio, DateTime? dataFim);
    }
}
