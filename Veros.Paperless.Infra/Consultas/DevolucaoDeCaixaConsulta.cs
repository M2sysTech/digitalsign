namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class DevolucaoDeCaixaConsulta : DaoBase, IDevolucaoDeCaixaConsulta
    {
        public IList<DevolucaoDeCaixa> Obter(string pacoteIdentificacao)
        {
            var sql = @"
SELECT p.pack_code Id, p.pack_status Status, pack_identificacao Identificacao, Count(b.batch_Code) AS TotalDossies 
FROM pack p inner join batch b ON p.pack_code = b.pack_code 
WHERE p.pack_status = 'X'";
 if (string.IsNullOrEmpty(pacoteIdentificacao) == false)
{
    sql = sql + " AND p.pack_identificacao = :pacoteIdentificacao";
}

sql = sql + " GROUP BY p.pack_code, p.pack_status, p.pack_identificacao";
sql = sql + " ORDER BY p.pack_identificacao";

            if (string.IsNullOrEmpty(pacoteIdentificacao) == false)
            {
                return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Status", NHibernateUtil.String)
                .AddScalar("Identificacao", NHibernateUtil.String)
                .AddScalar("TotalDossies", NHibernateUtil.Int32)                
                .SetParameter("pacoteIdentificacao", pacoteIdentificacao)
                .SetResultTransformer(CustomResultTransformer<DevolucaoDeCaixa>.Do())
                .List<DevolucaoDeCaixa>();                
            }

            return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Status", NHibernateUtil.String)
                .AddScalar("Identificacao", NHibernateUtil.String)
                .AddScalar("TotalDossies", NHibernateUtil.Int32)                
                .SetResultTransformer(CustomResultTransformer<DevolucaoDeCaixa>.Do())
                .List<DevolucaoDeCaixa>();            
        }
    }
}
