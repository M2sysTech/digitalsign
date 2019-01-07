namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class TotaisPorPacoteProcessadoConsulta : DaoBase, ITotaisPorPacoteProcessadoConsulta
    {
        public IList<TotaisPorPacoteProcessado> Obter()
        {
            const string Sql = @"
SELECT 
    p.pacoteprocessado_code PacoteProcessadoId
    , p.PACOTEPROCESSADO_RECEBIDOEM RecebidoEm
    , p.pacoteprocessado_status Status
    , p.pacoteprocessado_ativado Ativado
    , p.pacoteprocessado_qualicef ExibeNaQualidade
    , capturado.total Capturado
    , Nvl(ocr.total,0) EmOcr
    , Nvl(triagem.total,0) Triagem
    , Nvl(identmanual.total,0) IdentManual
    , Nvl(ocrpdfa.total,0) OcrPDFa
    , Nvl(class.total, 0) EmClassificacao   
    , Nvl(QualiM2.total, 0) EmQualidadeM2Sys
    , Nvl(Ajuste.total, 0) EmAjuste
    , Nvl(Assina.total, 0) EmAssinatura
    , Nvl(QualiCEF.total, 0) + Nvl(Fatura.total, 0) Concluido
    , Nvl(QualiCEF.total, 0) EmQualidadeCef
    , Nvl(Fatura.total, 0) EmFaturamento
FROM pacoteprocessado p
  left JOIN (
      SELECT Count(batch_code) total , pacoteprocessado_code
      FROM batch WHERE batch_status NOT IN ('*','10', '15','12','R5','DE') AND pacoteprocessado_code IS NOT NULL
      GROUP BY  pacoteprocessado_code
  ) capturado ON capturado.pacoteprocessado_code = p.pacoteprocessado_code
  left JOIN (
      SELECT Count(batch_code) total , pacoteprocessado_code
      FROM batch WHERE batch_status IN ('30', '35', '50', '5A', '45', '4A') AND pacoteprocessado_code IS NOT NULL
      GROUP BY  pacoteprocessado_code
  ) ocr ON ocr.pacoteprocessado_code = p.pacoteprocessado_code
  left JOIN (
      SELECT Count(DISTINCT b.batch_code) total , pacoteprocessado_code
      FROM batch B WHERE B.batch_status IN ('T5') AND pacoteprocessado_code IS NOT NULL
      GROUP BY pacoteprocessado_code
  ) triagem ON triagem.pacoteprocessado_code = p.pacoteprocessado_code
  left JOIN (
      SELECT Count(B.batch_code) total , pacoteprocessado_code
      FROM batch B
      INNER JOIN MDOC M ON B.BATCH_CODE = M.BATCH_CODE
      WHERE batch_status IN ('I5') AND pacoteprocessado_code IS NOT NULL
      AND M.TYPEDOC_ID = 990
      AND M.MDOC_STATUS != '*'
      AND M.MDOC_VIRTUAL = 0
      GROUP BY  pacoteprocessado_code        
  ) identmanual ON identmanual.pacoteprocessado_code = p.pacoteprocessado_code  
  left JOIN (
      SELECT Count(DISTINCT B.BATCH_CODE) TOTAL , pacoteprocessado_code
      FROM BATCH B
      INNER JOIN MDOC M ON B.BATCH_CODE = M.BATCH_CODE
      INNER JOIN DOC D ON M.MDOC_CODE = M.MDOC_CODE
      WHERE B.BATCH_STATUS = '55'
      AND M.MDOC_STATUS = '35'
      AND D.DOC_STATUS = '55'     
      AND D.DOC_FIMOCR IS NULL  
      GROUP BY  pacoteprocessado_code
  ) ocrPdfa ON ocrPdfa.pacoteprocessado_code = p.pacoteprocessado_code
  left JOIN (
      SELECT Count(DISTINCT B.BATCH_CODE) TOTAL , pacoteprocessado_code
      FROM BATCH B
      INNER JOIN MDOC M ON B.BATCH_CODE = M.BATCH_CODE
      WHERE B.BATCH_STATUS = '62'
      AND M.MDOC_STATUS != '*'
      AND M.MDOC_INDICIOfRAUDE IS NULL
      AND pacoteprocessado_code IS NOT NULL
      GROUP BY  pacoteprocessado_code
  ) Class ON class.pacoteprocessado_code = p.pacoteprocessado_code
  left JOIN (
      SELECT Count(batch_code) total , pacoteprocessado_code
      FROM batch WHERE batch_status IN ( 'M1','M5','MA') AND pacoteprocessado_code IS NOT NULL
      GROUP BY  pacoteprocessado_code
  ) QualiM2 ON QualiM2.pacoteprocessado_code = p.pacoteprocessado_code
  left JOIN (
      SELECT T1.TOTAL + Nvl(T2.TOTAL,0) AS TOTAL, T1.pacoteprocessado_code
      FROM
      (
        SELECT Count(batch_code) total , pacoteprocessado_code
        FROM batch WHERE batch_status IN ('J1', 'J5', 'J6', 'JA', 'JX', 'K5','KA','K1') AND pacoteprocessado_code IS NOT NULL
        GROUP BY  pacoteprocessado_code
      ) T1 LEFT JOIN
      (
        SELECT Count(DISTINCT b.batch_code) TOTAL, pacoteprocessado_code
        FROM batch b JOIN mdoc m ON m.batch_code = b.batch_code AND    MDOC_INDICIOFRAUDE  = 'DP'
        left JOIN mdoc m2 ON m2.batch_code = b.batch_code
        WHERE b.batch_status = '62' AND m2.mdoc_code IS NULL
        AND pacoteprocessado_code IS NOT NULL
        GROUP BY  pacoteprocessado_code
      ) T2 ON T2.pacoteprocessado_code = T1.pacoteprocessado_code
  ) Ajuste ON Ajuste.pacoteprocessado_code = p.pacoteprocessado_code
  left JOIN (
      SELECT Count(batch_code) total , pacoteprocessado_code
      FROM batch WHERE batch_status IN ( '82', '83','N1', 'N2') AND pacoteprocessado_code IS NOT NULL
      GROUP BY  pacoteprocessado_code
  ) Assina ON Assina.pacoteprocessado_code = p.pacoteprocessado_code
  left JOIN (
      SELECT Count(batch_code) total , pacoteprocessado_code
      FROM batch WHERE batch_status IN ( 'QA','Q5','Q1') AND pacoteprocessado_code IS NOT NULL
      GROUP BY  pacoteprocessado_code
  ) QualiCEF ON QualiCEF.pacoteprocessado_code = p.pacoteprocessado_code
  left JOIN (
      SELECT Count(batch_code) total , pacoteprocessado_code
      FROM batch WHERE batch_status IN ('H0','G0') AND pacoteprocessado_code IS NOT NULL
      GROUP BY  pacoteprocessado_code
  ) Fatura ON Fatura.pacoteprocessado_code = p.pacoteprocessado_code
  WHERE PACOTEPROCESSADO_RECEBIDOEM > TO_DATE('01/10/2017', 'DD/MM/YYYY')
  ORDER BY RecebidoEm
";
            return this.Session
                .CreateSQLQuery(Sql)
                .AddScalar("PacoteProcessadoId", NHibernateUtil.Int32)
                .AddScalar("RecebidoEm", NHibernateUtil.DateTime)
                .AddScalar("Status", NHibernateUtil.Int32)
                .AddScalar("Ativado", NHibernateUtil.AnsiString)
                .AddScalar("ExibeNaQualidade", NHibernateUtil.AnsiString)
                .AddScalar("Capturado", NHibernateUtil.Int32)
                .AddScalar("EmOcr", NHibernateUtil.Int32)
                .AddScalar("Triagem", NHibernateUtil.Int32)
                .AddScalar("IdentManual", NHibernateUtil.Int32)
                .AddScalar("OcrPdfa", NHibernateUtil.Int32)
                .AddScalar("EmClassificacao", NHibernateUtil.Int32)
                .AddScalar("EmQualidadeM2Sys", NHibernateUtil.Int32)
                .AddScalar("EmAjuste", NHibernateUtil.Int32)
                .AddScalar("EmAssinatura", NHibernateUtil.Int32)
                .AddScalar("Concluido", NHibernateUtil.Int32)
                .AddScalar("EmQualidadeCef", NHibernateUtil.Int32)
                .AddScalar("EmFaturamento", NHibernateUtil.Int32)
                .SetResultTransformer(CustomResultTransformer<TotaisPorPacoteProcessado>.Do())
                .List<TotaisPorPacoteProcessado>();
        }
    }
}
