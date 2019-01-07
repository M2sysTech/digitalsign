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
    public class RelatorioDeCaixasPorUniColRecArmConsulta : DaoBase, IRelatorioDeCaixasPorUniColRecArmConsulta
    {
        public IList<CaixaPorUniColRecArm> Obter(string dataInicio, string dataFim)
        {
            var sql = @"
SELECT 
    Sum (COLETA_QTD2) QuantidadeDeCaixas, 
    COLETA_ENDERECO Unidade, 
    to_Char(coleta_data, 'DD.MM.YYYY') DataColeta, 
    to_Char(COLETA_DTRECEPCAO, 'DD.MM.YYYY') DataRecepcao, 
    to_Char(COLETA_DTRECEPCAO, 'DD.MM.YYYY') DataArmazenamento
FROM 
    coleta
WHERE 
    coleta_data BETWEEN :dataInicio and :dataFim
GROUP BY 
    COLETA_ENDERECO, 
    to_Char (COLETA_DATA, 'DD.MM.YYYY'), 
    to_Char (COLETA_DTRECEPCAO, 'DD.MM.YYYY'), 
    to_Char (COLETA_DTRECEPCAO, 'DD.MM.YYYY')
ORDER BY  
    To_Date(to_Char(COLETA_DATA, 'DD.MM.YYYY'), 'DD.MM.YYYY') 
";

            return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("DataColeta", NHibernateUtil.Date)
                .AddScalar("DataRecepcao", NHibernateUtil.Date)
                .AddScalar("DataArmazenamento", NHibernateUtil.Date)
                .AddScalar("QuantidadeDeCaixas", NHibernateUtil.Int32)
                .AddScalar("Unidade", NHibernateUtil.String)
                .SetDateTime("dataInicio", DateTime.Parse(dataInicio))
                .SetDateTime("dataFim", DateTime.Parse(dataFim))
                .SetResultTransformer(CustomResultTransformer<CaixaPorUniColRecArm>.Do())
                .List<CaixaPorUniColRecArm>();
        }
    }
}