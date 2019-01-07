namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.ViewModel;
    using NHibernate;

    public class LogsAjusteConsulta : DaoBase, ILogsAjusteConsulta
    {
        public IList<LogDeAjusteViewModel> Obter(int loteId)
        {
            const string Sql = @"
SELECT 
    LOG_TIPO Tipo
    ,USR_CODE UsuarioId
    ,LOG_DATE Data
    ,LOG_OBS Observacao
    ,LOG_ACTION Acao
FROM (
SELECT 'LP' LOG_TIPO, 
  L.USR_CODE, 
  L.LOGPROC_DATE LOG_DATE, 
  CASE L.LOGPROC_ACTION
    WHEN 'FA' THEN 'Aprovado no Ajuste'
    WHEN 'SR' THEN 'Solicitado Recaptura no Ajuste: ' || L.LOGPROC_OBS    
    ELSE L.LOGPROC_OBS
  END LOG_OBS,
  L.LOGPROC_ACTION LOG_ACTION
FROM LOGPROC L
INNER JOIN PROC P ON L.PROC_CODE = P.PROC_CODE
WHERE L.LOGPROC_ACTION IN ('FA', 'SM', 'SR', 'QM', 'QC') 
AND P.BATCH_CODE = :loteId

UNION

SELECT 'LB' LOG_TIPO, 
  L.USR_CODE,
  L.LOGBATCH_DATE LOG_DATE,
  CASE L.LOGBATCH_ACTION
    WHEN 'SS' THEN L.LOGBATCH_OBS
    ELSE LOGBATCH_OBS
  END LOG_OBS,
  L.LOGBATCH_ACTION LOG_ACTION
FROM LOGBATCH L
WHERE L.LOGBATCH_ACTION IN ('SS')
AND L.BATCH_CODE = :loteId
)
ORDER BY Data
";

            return this.Session
            .CreateSQLQuery(Sql)
            .AddScalar("Tipo", NHibernateUtil.String)
            .AddScalar("UsuarioId", NHibernateUtil.Int32)
            .AddScalar("Data", NHibernateUtil.DateTime)
            .AddScalar("Observacao", NHibernateUtil.String)
            .AddScalar("Acao", NHibernateUtil.String)
            .SetParameter("loteId", loteId)
            .SetResultTransformer(CustomResultTransformer<LogDeAjusteViewModel>.Do())
            .List<LogDeAjusteViewModel>();
        }
    }
}
