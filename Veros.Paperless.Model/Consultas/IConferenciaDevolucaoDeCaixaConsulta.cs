namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IConferenciaDevolucaoDeCaixaConsulta
    {
        IList<ConferenciaDevolucaoDeCaixa> Obter(string pacoteIdentificacao);
    }
}
