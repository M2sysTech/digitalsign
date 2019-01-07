namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IAcompanhamentoProcessoDevolvidoOuLiberadoConsulta
    {
        IList<ProcessoDevolvidoOuLiberado> ObterPorOperador(DateTime dataInicio, DateTime dataFim, int usuarioId);

        IList<ProcessoDevolvidoOuLiberado> ObterPorAgenciaEConta(string agencia, string conta);
    }
}