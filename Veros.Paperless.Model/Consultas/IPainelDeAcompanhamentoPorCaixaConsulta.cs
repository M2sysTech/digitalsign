namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IPainelDeAcompanhamentoPorCaixaConsulta
    {
        IList<AcompanhamentoCaixa> Obter(int pacoteId);
    }
}