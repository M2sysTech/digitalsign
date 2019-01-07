namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class TotaisAguardandoQualidadeCefConsulta : DaoBase, ITotaisAguardandoQualidadeCefConsulta
    {
        public IList<TotaisAguardandoQualidadeCef> Obter(int loteCefId = 0)
        {
            var filtroPacote = loteCefId > 0 ?
                " AND L.LOTECEF_CODE=" + loteCefId :
                string.Empty;

            const string Sql = @"
SELECT 
    L.LOTECEF_CODE LoteCefId,
    L.LOTECEF_STATUS LoteCefStatus,
    L.LOTECEF_DTFIM LoteCefData,
    L.LOTECEF_DTAPROV LoteCefAprovacao,
    L.LOTECEF_DTASSINACERTIFIC LoteCefAssinatura, 
    B.BATCH_QUALICEF TipoDeAmostra,
    Count(B.BATCH_CODE) TotalPacote,
    Sum(CASE WHEN B.BATCH_RESULTQCEF = 'M' THEN 1 ELSE 0 END) QtdMarcados,
    Sum(CASE WHEN B.BATCH_RESULTQCEF = 'A' THEN 1 ELSE 0 END) QtdAprovados,
    Sum(CASE WHEN B.BATCH_QUALICEF >= 1 AND B.BATCH_STATUS NOT IN ('QA', 'Q5', 'H0') THEN 1 ELSE 0 END) QtdEmReprocessamento,
    Sum(CASE WHEN B.BATCH_QUALICEF > 0 THEN 1 ELSE 0 END) QtdSelecionados
FROM 
    LOTECEF L
    INNER JOIN BATCH B ON L.LOTECEF_CODE = B.LOTECEF_CODE
WHERE 
    B.BATCH_STATUS != '*'    
    AND L.LOTECEF_STATUS IN ('FE', 'RP', 'AP', 'RN') 
    AND L.LOTECEF_VISIVEL = 1
    {0}
GROUP BY 
    L.LOTECEF_CODE, L.LOTECEF_STATUS, L.LOTECEF_DTFIM, L.LOTECEF_DTAPROV, L.LOTECEF_DTASSINACERTIFIC, B.BATCH_QUALICEF
ORDER BY 
    L.LOTECEF_DTFIM desc
";

            return this.Session
                .CreateSQLQuery(string.Format(Sql, filtroPacote))
                .AddScalar("LoteCefId", NHibernateUtil.Int32)
                .AddScalar("LoteCefStatus", NHibernateUtil.AnsiString)
                .AddScalar("LoteCefData", NHibernateUtil.Date)
                .AddScalar("LoteCefAprovacao", NHibernateUtil.Date)
                .AddScalar("LoteCefAssinatura", NHibernateUtil.Date)
                .AddScalar("TipoDeAmostra", NHibernateUtil.Int32)
                .AddScalar("TotalPacote", NHibernateUtil.Int32)
                .AddScalar("QtdMarcados", NHibernateUtil.Int32)
                .AddScalar("QtdAprovados", NHibernateUtil.Int32)
                .AddScalar("QtdEmReprocessamento", NHibernateUtil.Int32)
                .AddScalar("QtdSelecionados", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<TotaisAguardandoQualidadeCef>.Do())
                .List<TotaisAguardandoQualidadeCef>();
        }
    }
}
