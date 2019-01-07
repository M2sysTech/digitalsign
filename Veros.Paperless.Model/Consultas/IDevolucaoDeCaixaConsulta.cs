namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IDevolucaoDeCaixaConsulta
    {
        IList<DevolucaoDeCaixa> Obter(string pacoteIdentificacao);
    }
}
