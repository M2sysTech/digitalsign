namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class ContasLiberadasSemAprovacaoConsulta : DaoBase, IContasLiberadasSemAprovacaoConsulta
    {
        public IList<ContaLiberadaSemAprovacao> Obter(DateTime dataInicio, DateTime dataFim)
        {
            const string Sql = @"
SELECT P.PROC_CONTA Conta, P.PROC_AGENCIA Agencia, B.PACOTEPROCESSADO_CODE PacoteId, B.BATCH_CODE LoteId, B.BATCH_DT LoteData, R.REMESSA_EXTENSAO ExtensaoRemessa
FROM PROC P
INNER JOIN BATCH B ON P.BATCH_CODE = B.BATCH_CODE
LEFT JOIN LOGPROC LP ON P.PROC_CODE = LP.PROC_CODE AND LP.LOGPROC_ACTION IN ('LA', 'DA')
LEFT JOIN REMESSA R ON P.PROC_CODE = R.PROC_CODE
WHERE B.BATCH_TAPROV IS NOT NULL
AND LP.LOGPROC_CODE IS NULL
AND B.BATCH_DT BETWEEN :dataInicio AND :dataFim

UNION ALL

SELECT P.PROC_CONTA Conta, P.PROC_AGENCIA Agencia, B.PACOTEPROCESSADO_CODE PacoteId, B.BATCH_CODE LoteId, B.BATCH_DT LoteData, R.REMESSA_EXTENSAO ExtensaoRemessa 
FROM PROC_BK P
INNER JOIN BATCH_BK B ON P.BATCH_CODE = B.BATCH_CODE
LEFT JOIN LOGPROC_BK LP ON P.PROC_CODE = LP.PROC_CODE AND LP.LOGPROC_ACTION IN ('LA', 'DA')
LEFT JOIN REMESSA R ON P.PROC_CODE = R.PROC_CODE
WHERE B.BATCH_TAPROV IS NOT NULL
AND LP.LOGPROC_CODE IS NULL
AND B.BATCH_DT BETWEEN :dataInicio AND :dataFim
";

            return this.Session
                .CreateSQLQuery(Sql)
                .AddScalar("Conta", NHibernateUtil.String)
                .AddScalar("Agencia", NHibernateUtil.String)
                .AddScalar("PacoteId", NHibernateUtil.Int32)
                .AddScalar("LoteId", NHibernateUtil.Int32)
                .AddScalar("LoteData", NHibernateUtil.DateTime)
                .AddScalar("ExtensaoRemessa", NHibernateUtil.String)
                .SetDateTime("dataInicio", dataInicio)
                .SetDateTime("dataFim", dataFim)
                .SetResultTransformer(CustomResultTransformer<ContaLiberadaSemAprovacao>.Do())
                .List<ContaLiberadaSemAprovacao>();
        }
    }
}