namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.ViewModel;
    using NHibernate;

    public class ProdutividadeOperadorConsulta : DaoBase, IProdutividadeOperadorConsulta
    {
        public IList<ProdutividadeOperadorViewModel> Obter(string dataInicio, string dataFim)
        {
            const string Sql = @"
SELECT * FROM (
 SELECT U.USR_MATRICULA Matricula, U.USR_NAME Nome, 
   (SELECT TOTAL FROM 
    (
    SELECT Count(1) TOTAL, USR_CODE FROM 
      (
          SELECT LB.BATCH_CODE, LB.USR_CODE FROM LOGBATCH LB WHERE LOGBATCH_ACTION = 'CF'
          AND LB.LOGBATCH_OBS LIKE 'Lote capturado no IP (%'
          AND LB.LOGBATCH_DATE BETWEEN :dataInicio AND :dataFim  
          GROUP BY LB.BATCH_CODE, LB.USR_CODE, To_Char(LB.LOGBATCH_DATE, 'DD/MM/YYYY HH24')
      ) GROUP BY USR_CODE  
    )
    WHERE usr_code = U.usr_code
   ) QuantidadeCapturaRecaptura,   
  (SELECT Count(0) TOTAL 
  FROM PACK L
  WHERE L.PACK_DTPREPARO BETWEEN :dataInicio AND :dataFim
  AND L.USR_PREPARO = U.USR_CODE) QuantidadePreparo,
  (SELECT Count(0) TOTAL 
  FROM LOGBATCH L
  WHERE L.LOGBATCH_ACTION = 'TR' 
  AND L.LOGBATCH_DATE BETWEEN :dataInicio AND :dataFim
  AND L.USR_CODE = U.USR_CODE) QuantidadeTriagem,
  (SELECT Count(0) TOTAL 
  FROM LOGPROC L
  WHERE L.LOGPROC_ACTION = 'QM' 
  AND L.LOGPROC_DATE BETWEEN :dataInicio AND :dataFim
  AND L.USR_CODE = U.USR_CODE) QuantidadeQualidadeM2,
  (SELECT Count(0) TOTAL  
  FROM LOGMDOC L
  WHERE L.LOGMDOC_ACTION IN ('AF','RE', 'DP')
  AND L.LOGMDOC_DATE BETWEEN :dataInicio AND :dataFim
  AND L.USR_CODE = U.USR_CODE) QuantidadeFormalistica,
  (SELECT Count(0) TOTAL  
  FROM LOGPROC L
  WHERE L.LOGPROC_ACTION IN ('FA', 'FL')
  AND L.LOGPROC_DATE BETWEEN :dataInicio AND :dataFim
  AND L.USR_CODE = U.USR_CODE) QuantidadeAjuste,
  (SELECT Count(0) TOTAL 
  FROM LOGBATCH L
  WHERE L.LOGBATCH_ACTION = 'SS'
  AND L.LOGBATCH_DATE BETWEEN :dataInicio AND :dataFim
  AND L.USR_CODE = U.USR_CODE) QuantidadeAjusteReparacao    
FROM USR U
) T
WHERE QuantidadeCapturaRecaptura> 0 OR QuantidadePreparo > 0 OR QuantidadeTriagem > 0 OR 
QuantidadeQualidadeM2 > 0 OR QuantidadeFormalistica > 0 OR QuantidadeAjuste > 0 OR 
QuantidadeAjusteReparacao > 0   
";
            return this.Session
            .CreateSQLQuery(Sql)
            .AddScalar("Matricula", NHibernateUtil.String)
            .AddScalar("Nome", NHibernateUtil.String)
            .AddScalar("QuantidadePreparo", NHibernateUtil.Int32)
            .AddScalar("QuantidadeCapturaRecaptura", NHibernateUtil.Int32)
            .AddScalar("QuantidadeTriagem", NHibernateUtil.Int32)
            .AddScalar("QuantidadeQualidadeM2", NHibernateUtil.Int32)
            .AddScalar("QuantidadeFormalistica", NHibernateUtil.Int32)
            .AddScalar("QuantidadeAjuste", NHibernateUtil.Int32)
            .AddScalar("QuantidadeAjusteReparacao", NHibernateUtil.Int32)
            .SetDateTime("dataInicio", DateTime.Parse(dataInicio))
            .SetDateTime("dataFim", DateTime.Parse(dataFim).AddDays(1))
            .SetResultTransformer(CustomResultTransformer<ProdutividadeOperadorViewModel>.Do())
            .List<ProdutividadeOperadorViewModel>();
        }
    }
}
