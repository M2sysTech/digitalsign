namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IAnaliseDevolucaoDeCaixaConsulta
    {
        IList<AnaliseDevolucaoDeCaixa> Obter(string pacoteIdentificacao);
    }
}
