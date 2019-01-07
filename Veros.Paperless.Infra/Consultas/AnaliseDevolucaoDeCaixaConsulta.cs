namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class AnaliseDevolucaoDeCaixaConsulta : DaoBase, IAnaliseDevolucaoDeCaixaConsulta
    {
        public IList<AnaliseDevolucaoDeCaixa> Obter(string pacoteIdentificacao)
        {
            var sql = @"
SELECT pack_code Id, pack_identificacao Identificacao, Processados + Excluidos + EmProcessamento AS TotalDossies, 
Excluidos, Processados, EmProcessamento, Aprovados 
FROM 
(
  SELECT k.pack_code, k.pack_Identificacao, Nvl(t1.Processados,0) Processados, Nvl(t2.Excluidos,0) Excluidos, Nvl(t3.EmProcessamento, 0) EmProcessamento, Nvl(t4.Aprovados, 0) Aprovados FROM pack k  
  left JOIN 
  (
    SELECT k1.pack_code, Count(b.batch_code ) Processados
    FROM pack k1 
      JOIN batch b ON b.pack_code = k1.pack_code    
    WHERE
      k1.pack_status = 'C'
      AND b.batch_status IN ('H0','Q5')
    GROUP BY k1.pack_code
  ) t1 ON k.pack_code = t1.pack_code  
  left JOIN 
  (
    SELECT  k1.pack_code, Count(b.batch_code ) Excluidos
    FROM pack k1 
      JOIN batch b ON b.pack_code = k1.pack_code    
    WHERE
      k1.pack_status = 'C'
      AND b.batch_status = '*'
    GROUP BY k1.pack_code
  ) t2 ON  k.pack_code = t2.pack_code
  left JOIN 
  (
    SELECT  k1.pack_code, Count(b.batch_code ) EmProcessamento
    FROM pack k1 
      JOIN batch b ON b.pack_code = k1.pack_code    
    WHERE
      k1.pack_status = 'C'
      AND b.batch_status NOT IN ('*', 'H0', 'Q5')
    GROUP BY k1.pack_code
  ) t3 ON  k.pack_code = t3.pack_code
  left JOIN 
  (
    SELECT  k1.pack_code, Count(b.batch_code ) Aprovados 
    FROM pack k1 
      JOIN batch b ON b.pack_code = k1.pack_code    
      JOIN pacoteprocessado p ON p.pacoteprocessado_code = b.pacoteprocessado_code
    WHERE
      k1.pack_status = 'C'
      AND pacoteprocessado_status = 5
      AND b.batch_status <> '*'
    GROUP BY k1.pack_code
  ) t4 ON  k.pack_code = t4.pack_code 
  WHERE 
    k.pack_status = 'C'
    AND    Nvl(t1.Processados,0) +  Nvl(t2.Excluidos,0) +  Nvl(t3.EmProcessamento, 0) > 0";

 if (string.IsNullOrEmpty(pacoteIdentificacao) == false)
{
    sql = sql + "AND k.pack_identificacao = :pacoteIdentificacao";
}

sql = sql + ") ORDER BY aprovados DESC, pack_code";

            if (string.IsNullOrEmpty(pacoteIdentificacao) == false)
            {
                return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Identificacao", NHibernateUtil.String)
                .AddScalar("TotalDossies", NHibernateUtil.Int32)
                .AddScalar("Excluidos", NHibernateUtil.Int32)
                .AddScalar("Processados", NHibernateUtil.Int32)
                .AddScalar("EmProcessamento", NHibernateUtil.Int32)
                .AddScalar("Aprovados", NHibernateUtil.Int32)
                .SetParameter("pacoteIdentificacao", pacoteIdentificacao)
                .SetResultTransformer(CustomResultTransformer<AnaliseDevolucaoDeCaixa>.Do())
                .List<AnaliseDevolucaoDeCaixa>();                
            }

            return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Identificacao", NHibernateUtil.String)
                .AddScalar("TotalDossies", NHibernateUtil.Int32)
                .AddScalar("Excluidos", NHibernateUtil.Int32)
                .AddScalar("Processados", NHibernateUtil.Int32)
                .AddScalar("EmProcessamento", NHibernateUtil.Int32)
                .AddScalar("Aprovados", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<AnaliseDevolucaoDeCaixa>.Do())
                .List<AnaliseDevolucaoDeCaixa>();            
        }
    }
}
