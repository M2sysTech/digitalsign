namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    /// <summary>
    /// TODO: escrever teste
    /// </summary>
    public class RelatorioDeQuantitativoDeAssinaturaDigitalConsulta : DaoBase, IRelatorioDeQuantitativoDeAssinaturaDigitalConsulta
    {
        public IList<QuantitativoDeAssinaturaDigital> Obter(string dataInicio, string dataFim)
        {
            var sql = @"
SELECT trunc(CARIMBOCONSUMIDO_EM) Data, COUNT(CARIMBOCONSUMIDO_CODE) Quantidade
FROM CARIMBOCONSUMIDO
WHERE
CARIMBOCONSUMIDO_EM BETWEEN :dataInicio and :dataFim
GROUP BY trunc(CARIMBOCONSUMIDO_EM)
ORDER BY trunc(CARIMBOCONSUMIDO_EM)";

            return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("Data", NHibernateUtil.Date)
                .AddScalar("Quantidade", NHibernateUtil.Int32)
                .SetDateTime("dataInicio", DateTime.Parse(dataInicio))
                .SetDateTime("dataFim", DateTime.Parse(dataFim))
                .SetResultTransformer(CustomResultTransformer<QuantitativoDeAssinaturaDigital>.Do())
                .List<QuantitativoDeAssinaturaDigital>();
        }
    }
}