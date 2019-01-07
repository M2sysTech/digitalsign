namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface IRelatorioDeCaixasPorUniColRecArmConsulta
    {
        IList<CaixaPorUniColRecArm> Obter(string dataInicio, string dataFim);
    }
}
