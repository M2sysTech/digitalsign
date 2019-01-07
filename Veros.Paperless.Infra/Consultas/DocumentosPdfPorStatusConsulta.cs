namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class DocumentosPdfPorStatusConsulta : DaoBase, IDocumentosPdfPorStatusConsulta
    {
        public IList<DocumentosPdfConsulta> Obter(string statusConsulta)
        {
            var sql = @"
SELECT DISTINCT d.doc_code PaginaId, d.mdoc_code DocumentoId, d.batch_code LoteId, d.DOC_DTCENTERID DataCenterId
FROM 
  doc d
  INNER JOIN mdoc m ON m.mdoc_code = d.mdoc_code
  INNER JOIN batch b ON b.batch_code = d.batch_code
WHERE
  m.mdoc_virtual = 1
  AND d.doc_filetype = 'PDF'
  AND m.mdoc_status <> '*'
  AND b.batch_status = :statusConsulta
";

            var documentosPdfConsulta = this.Session
                .CreateSQLQuery(sql)
                .AddScalar("PaginaId", NHibernateUtil.Int32)
                .AddScalar("DocumentoId", NHibernateUtil.Int32)
                .AddScalar("LoteId", NHibernateUtil.Int32)
                .AddScalar("DataCenterId", NHibernateUtil.Int32)
                .SetParameter("statusConsulta", statusConsulta)
                .SetResultTransformer(CustomResultTransformer<DocumentosPdfConsulta>.Do())
                .List<DocumentosPdfConsulta>();

            return documentosPdfConsulta;
        }
    }
}