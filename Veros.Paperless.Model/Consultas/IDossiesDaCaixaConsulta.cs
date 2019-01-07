namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IDossiesDaCaixaConsulta
    {
        IList<DossiesDaCaixa> Obter(int pacoteId);
    }
}
