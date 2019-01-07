namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface ITotalDeLotesPorSituacaoAmostragemConsulta
    {
        IList<TotalDeLotesPorPacotePorSituacaoAmostragem> Obter(int loteCefId);
    }
}
