namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class DocumentosPdfPorStatusParaRecontagemPaginasConsulta : DaoBase, IDocumentosPdfPorStatusParaRecontagemPaginasConsulta
    {
        public IList<DocumentoRecontar> Obter(string statusConsulta)
        {
            var sql = @"
SELECT 
    DISTINCT MDOC_CODE Id
FROM 
    MDOC MD
    INNER JOIN BATCH B ON B.BATCH_CODE = MD.BATCH_CODE
WHERE 
    MD.TYPEDOC_ID NOT IN (121, 75)
    AND MDOC_VIRTUAL = 1
    AND MDOC_STATUS <> '*'
    AND (MDOC_RECONTADO = 0 OR MDOC_RECONTADO IS NULL)
    AND (MDOC_QTDEPAG = 0 OR MDOC_QTDEPAG IS NULL) 
    AND MD.BATCH_CODE NOT IN (SELECT BATCH_CODE FROM BATCH_BKPCEF)
    AND B.BATCH_STATUS = :statusConsulta
";

            var documentosPdfConsulta = this.Session
                .CreateSQLQuery(sql)
                .AddScalar("Id", NHibernateUtil.Int32)
                .SetParameter("statusConsulta", statusConsulta)
                .SetResultTransformer(CustomResultTransformer<DocumentoRecontar>.Do())
                .List<DocumentoRecontar>();

            return documentosPdfConsulta;
        }
    }
}