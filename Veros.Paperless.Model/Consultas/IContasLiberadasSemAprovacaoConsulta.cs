namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IContasLiberadasSemAprovacaoConsulta
    {
        IList<ContaLiberadaSemAprovacao> Obter(DateTime dataInicio, DateTime dataFim);
    }
}
