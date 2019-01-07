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
    public class RelatorioDeCaixasETotalDeDossiesConsulta : DaoBase, IRelatorioDeCaixasETotalDeDossiesConsulta
    {
        public IList<CaixaETotalDeDossies> Obter(string dataInicio, string dataFim)
        {
            var sql = @"
SELECT 
    t1.b DataColeta, t1.a QuantidadeDeCaixas, t2.a QuantidadeDeDossies
FROM  
    (SELECT 
        Count(p.PACK_CODE) a,to_Char(coleta_data, 'DD.MM.YYYY') b 
    FROM 
        pack p 
        INNER JOIN coleta c ON p.coleta_code = c.coleta_code
    GROUP BY to_Char (coleta_data, 'DD.MM.YYYY') 
    ORDER BY To_Date(to_Char(coleta_data, 'DD.MM.YYYY'), 'DD.MM.YYYY')) t1
JOIN   
    (SELECT 
        Count(pr.proc_CODE) a,to_Char(coleta_data, 'DD.MM.YYYY') b 
        FROM pack p 
        INNER JOIN coleta c ON p.coleta_code = c.coleta_code
        INNER JOIN batch b ON p.pack_code = b.pack_code
        INNER JOIN proc pr ON b.batch_code = pr.batch_code
    WHERE 
        pr.proc_status <> '*'
        AND coleta_data BETWEEN :dataInicio and :dataFim
    GROUP BY to_Char (coleta_data, 'DD.MM.YYYY') 
    ORDER BY To_Date(to_Char(coleta_data, 'DD.MM.YYYY'), 'DD.MM.YYYY')) t2   
ON t1.b = t2.b
";

            return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("DataColeta", NHibernateUtil.Date)
                .AddScalar("QuantidadeDeCaixas", NHibernateUtil.Int32)
                .AddScalar("QuantidadeDeDossies", NHibernateUtil.Int32)
                .SetDateTime("dataInicio", DateTime.Parse(dataInicio))
                .SetDateTime("dataFim", DateTime.Parse(dataFim))
                .SetResultTransformer(CustomResultTransformer<CaixaETotalDeDossies>.Do())
                .List<CaixaETotalDeDossies>();
        }
    }
}