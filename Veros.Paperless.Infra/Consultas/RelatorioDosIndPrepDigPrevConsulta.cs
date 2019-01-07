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
    public class RelatorioDosIndPrepDigPrevConsulta : DaoBase, IRelatorioDosIndPrepDigPrevConsulta
    {
        public IList<DossieIndPrepDigPrev> Obter()
        {
            var sql = @"
SELECT 
    t1.c1 Caixas, t2.c2 DossiesFinalizado, t2.c3 PaginasDossiesFinalizado,
    t3.c4 DossiesAguardandoDig, t3.c5 PaginasDossiesAguardandoDig,
    t4.c6 DossiesEmPreparo, t5.c7 DossiesPrevisto
FROM
    (
        SELECT 
            Sum(COLETA_QTD1) c1 
        FROM 
            coleta
    ) t1,
    (
        SELECT 
            Count(*) c2, Count(doc_code) c3 
        FROM 
            batch b
        JOIN 
            doc d ON b.batch_code = d.batch_code 
        WHERE 
            BATCH_STATUS = 'G0'
    ) t2,
    (
        SELECT 
            Count(*) c4, Count(doc_code) c5 
        FROM 
            batch b
        JOIN 
            doc d ON b.batch_code = d.batch_code 
        WHERE 
            BATCH_STATUS = '10'
    ) t3,
    (
        select 
            Count(*) c6 
        FROM 
            dossieesperado 
        WHERE 
            DOSSIEESPERADO_STATUS = 'E'
    ) t4,
    (
        select 
            Count(*) c7 
        FROM 
            dossieesperado 
        WHERE 
            DOSSIEESPERADO_STATUS = 'N'
    ) t5 
";

            return this.Session
                .CreateSQLQuery(sql)
                .AddScalar("Caixas", NHibernateUtil.Int32)
                .AddScalar("DossiesFinalizado", NHibernateUtil.Int32)
                .AddScalar("PaginasDossiesFinalizado", NHibernateUtil.Int32)
                .AddScalar("DossiesAguardandoDig", NHibernateUtil.Int32)
                .AddScalar("PaginasDossiesAguardandoDig", NHibernateUtil.Int32)
                .AddScalar("DossiesEmPreparo", NHibernateUtil.Int32)
                .AddScalar("DossiesPrevisto", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<DossieIndPrepDigPrev>.Do())
                .List<DossieIndPrepDigPrev>();
        }
    }
}