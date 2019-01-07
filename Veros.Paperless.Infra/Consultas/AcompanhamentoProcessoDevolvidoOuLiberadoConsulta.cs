namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class AcompanhamentoProcessoDevolvidoOuLiberadoConsulta : DaoBase, IAcompanhamentoProcessoDevolvidoOuLiberadoConsulta
    {
        public IList<ProcessoDevolvidoOuLiberado> ObterPorOperador(DateTime dataInicio, DateTime dataFim, int usuarioId)
        {
            const string Sql = @"
SELECT 
    P.BATCH_CODE LoteId, P.PROC_CODE ProcessoId, P.PROC_AGENCIA Agencia, P.PROC_CONTA Conta, 
    LP.LOGPROC_DATE DataOperacao, LP.LOGPROC_ACTION AcaoRealizada, U.USR_NAME Operador  
FROM LOGPROC LP
INNER JOIN PROC P ON P.PROC_CODE = LP.PROC_CODE 
INNER JOIN USR U ON U.USR_CODE = LP.USR_CODE 
WHERE LOGPROC_ACTION IN ('DA','LA')
    AND LP.LOGPROC_DATE BETWEEN  :dataInicio AND :dataFim
    AND (U.USR_CODE = :usuarioId OR :usuarioId = 0)
ORDER BY P.PROC_AGENCIA, P.PROC_CONTA, LP.PROC_CODE ASC 
";
            return this.Session.CreateSQLQuery(Sql)
               .AddScalar("LoteId", NHibernateUtil.Int32)
               .AddScalar("ProcessoId", NHibernateUtil.Int32)
               .AddScalar("Agencia", NHibernateUtil.String)
               .AddScalar("Conta", NHibernateUtil.String)
               .AddScalar("DataOperacao", NHibernateUtil.DateTime)
               .AddScalar("AcaoRealizada", NHibernateUtil.String)
               .AddScalar("Operador", NHibernateUtil.String)
               .SetParameter("dataInicio", dataInicio)
               .SetParameter("dataFim", dataFim)
               .SetParameter("usuarioId", usuarioId)
               .SetResultTransformer(CustomResultTransformer<ProcessoDevolvidoOuLiberado>.Do())
               .List<ProcessoDevolvidoOuLiberado>();
        }

        public IList<ProcessoDevolvidoOuLiberado> ObterPorAgenciaEConta(string agencia, string conta)
        {
            const string Sql = @"
SELECT 
    P.BATCH_CODE LoteId, P.PROC_CODE ProcessoId, P.PROC_AGENCIA Agencia, P.PROC_CONTA Conta, 
    LP.LOGPROC_DATE DataOperacao, LP.LOGPROC_ACTION AcaoRealizada, U.USR_NAME Operador  
FROM LOGPROC LP
INNER JOIN PROC P ON P.PROC_CODE = LP.PROC_CODE 
INNER JOIN USR U ON U.USR_CODE = LP.USR_CODE 
WHERE LOGPROC_ACTION IN ('DA','LA')
    AND P.PROC_AGENCIA = :agencia
    AND P.PROC_CONTA = :conta
ORDER BY P.PROC_AGENCIA, P.PROC_CONTA, LP.PROC_CODE ASC 
";
            return this.Session.CreateSQLQuery(Sql)
               .AddScalar("LoteId", NHibernateUtil.Int32)
               .AddScalar("ProcessoId", NHibernateUtil.Int32)
               .AddScalar("Agencia", NHibernateUtil.String)
               .AddScalar("Conta", NHibernateUtil.String)
               .AddScalar("DataOperacao", NHibernateUtil.DateTime)
               .AddScalar("AcaoRealizada", NHibernateUtil.String)
               .AddScalar("Operador", NHibernateUtil.String)
               .SetParameter("agencia", agencia)
               .SetParameter("conta", conta)
               .SetResultTransformer(CustomResultTransformer<ProcessoDevolvidoOuLiberado>.Do())
               .List<ProcessoDevolvidoOuLiberado>();
        }
    }
}