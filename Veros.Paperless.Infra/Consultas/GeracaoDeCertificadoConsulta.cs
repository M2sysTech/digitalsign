namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.Entidades;

    public class GeracaoDeCertificadoConsulta : DaoBase, IGeracaoDeCertificadoConsulta
    {
        public IList<CertificadoDeEntregaConsulta> Obter()
        {
            var sql = @"
SELECT 
    LC.LOTECEF_CODE AS LoteCefId, LC.LOTECEF_QTBATCH AS QuantidadesDossies, LC.LOTECEF_DTFIM AS DataFim, 
    SUM(MD.MDOC_QTDEPAG) AS QuantidadeDePaginas, 
    LC.LOTECEF_DTGERACERTIFIC AS DataGeracaoCertificado, 
    LC.LOTECEF_DTASSINACERTIFIC AS DataAssinaturaCertificado 
FROM 
    MDOC MD 
INNER JOIN BATCH B ON B.BATCH_CODE=MD.BATCH_CODE
INNER JOIN LOTECEF LC ON LC.LOTECEF_CODE=B.LOTECEF_CODE
WHERE 
    LC.LOTECEF_STATUS = :status
    AND MD.MDOC_STATUS <> '*'
    AND MD.MDOC_VIRTUAL = 1
GROUP BY LC.LOTECEF_CODE, LC.LOTECEF_QTBATCH, LC.LOTECEF_DTFIM, LC.LOTECEF_DTGERACERTIFIC, LC.LOTECEF_DTASSINACERTIFIC";

            return this.Session
                .CreateSQLQuery(sql)
                .SetParameter("status", LoteCefStatus.AprovadoNaQualidade.Value)
                .SetResultTransformer(CustomResultTransformer<CertificadoDeEntregaConsulta>.Do())
                .List<CertificadoDeEntregaConsulta>();
        }
    }
}
