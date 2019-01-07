namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface ITotaisAguardandoQualidadeCefConsulta
    {
        IList<TotaisAguardandoQualidadeCef> Obter(int loteCefId = 0);
    }
}
