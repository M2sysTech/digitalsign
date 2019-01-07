namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.Entidades;
    using NHibernate;

    public class ObtemLotesParaAmostragemQualidadeCefConsulta : DaoBase, IObtemLotesParaAmostragemQualidadeCefConsulta
    {
        public IList<CodigoView> Executar(int loteCefId, int quantidade)
        {
            const string Sql = @"
    SELECT BATCH_CODE Codigo
    FROM (
        SELECT B.BATCH_CODE
        FROM BATCH B
        WHERE B.BATCH_STATUS != '*'
        AND B.BATCH_QUALICEF = 0
        AND B.LOTECEF_CODE = :loteCefId
        AND B.BATCH_TDC IS NULL 
        AND B.BATCH_STATUS = :status
        ORDER BY DBMS_RANDOM.VALUE)
    WHERE ROWNUM <= :quantidade";

            return this.Session
                .CreateSQLQuery(Sql)
                .AddScalar("Codigo", NHibernateUtil.Int32)
                .SetParameter("loteCefId", loteCefId)
                .SetParameter("status", LoteStatus.Faturamento.Value)
                .SetParameter("quantidade", quantidade)
                .SetResultTransformer(CustomResultTransformer<CodigoView>.Do())
                .List<CodigoView>();
        }
    }
}
