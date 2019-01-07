namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class TotalDeLotesPorSituacaoAmostragemConsulta : DaoBase, ITotalDeLotesPorSituacaoAmostragemConsulta
    {
        public IList<TotalDeLotesPorPacotePorSituacaoAmostragem> Obter(int loteCefId)
        {
            const string Sql = @"
SELECT Count(0) Total, BATCH_QUALICEF SituacaoAmostragem 
FROM BATCH 
WHERE BATCH_STATUS != '*'
    AND LOTECEF_CODE = :loteCefId 
GROUP BY BATCH_QUALICEF
";

            return this.Session
                .CreateSQLQuery(Sql)
                .AddScalar("Total", NHibernateUtil.Int32)
                .AddScalar("SituacaoAmostragem", NHibernateUtil.Int32)
                .SetParameter("loteCefId", loteCefId)
                .SetResultTransformer(CustomResultTransformer<TotalDeLotesPorPacotePorSituacaoAmostragem>.Do())
                .List<TotalDeLotesPorPacotePorSituacaoAmostragem>();
        }
    }
}
