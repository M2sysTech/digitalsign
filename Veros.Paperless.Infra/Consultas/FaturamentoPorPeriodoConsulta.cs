namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;

    public class FaturamentoPorPeriodoConsulta : DaoBase, IFaturamentoPorPeriodoConsulta
    {
        public IList<FaturamentoPorPeriodo> Obter(DateTime dataInicial, DateTime dataFinal)
        {
            var sql = @"
SELECT
--PRIMEIRA PARTE
  To_Char(Recepcionadas.dataRec,'dd/mm/yyyy') DataDoPacote , To_Char(Recepcionadas.dataRec,'HH24:MI:SS') HoraRecepcaoDoPacote, To_Char(Recepcionadas.dataImport,'HH24:MI:SS') HoraImportacaoDoPacote ,
  Recepcionadas.Recepcionadas TotalRecepcionadas,
  Nvl(SLA_OK_2Horas.SLA_OK_2Horas,0), TRUNC(Nvl(SLA_OK_2Horas.SLA_OK_2Horas,0)/Recepcionadas.Recepcionadas * 100,2) PORCENTAGEM,
  Nvl(SLA_OK_19Horas.SLA_OK_19Horas,0), TRUNC(Nvl(SLA_OK_19Horas.SLA_OK_19Horas,0)/Recepcionadas.Recepcionadas * 100,2) PORCENTAGEM,
  Nvl(SLA_OK_APOS17Horas.SLA_OK_APOS17Horas,0), TRUNC(Nvl(SLA_OK_APOS17Horas.SLA_OK_APOS17Horas,0)/Recepcionadas.Recepcionadas * 100,2) PORCENTAGEM,
  Nvl(SLA_NOK_DIAANTERIOR.SLA_NOK_DIAANTERIOR,0)  + Nvl(SLA_NOK_DIA.SLA_NOK_DIA ,0) ForaSla,
  TRUNC((Nvl(SLA_NOK_DIAANTERIOR.SLA_NOK_DIAANTERIOR,0)  + Nvl(SLA_NOK_DIA.SLA_NOK_DIA ,0))/Recepcionadas.Recepcionadas * 100,2) PORCENTAGEM,
  Nvl(Erro.Erro,0) + Nvl(INCONSISTENTES.INCONSISTENTES,0) ERRO, Trunc(ERRO/Recepcionadas.Recepcionadas * 100,2)PORCENTAGEM,

--SEGUNDA PARTE
       Nvl(ReconhecimentoAutomatico.ReconhecimentoAutomatico,0), TRUNC(Nvl(ReconhecimentoAutomatico.ReconhecimentoAutomatico,0)/Recepcionadas.Recepcionadas * 100,2) PORCENTAGEM,
       Nvl(AnaliseManual.AnaliseManual,0), TRUNC(Nvl(AnaliseManual.AnaliseManual,0)/Recepcionadas.Recepcionadas * 100,2) PORCENTAGEM,
       Nvl(AlteracaoCadastral.AlteracaoCadastral,0),TRUNC(Nvl(AlteracaoCadastral.AlteracaoCadastral,0)/Recepcionadas.Recepcionadas * 100,2) PORCENTAGEM,
       Nvl(AtuacaoBanco.AtuacaoBanco,0), TRUNC(Nvl(AtuacaoBanco.AtuacaoBanco,0)/Recepcionadas.Recepcionadas * 100,2) PORCENTAGEM,

--TERCEIRA PARTE
  Nvl(Liberadas.Liberadas,0), TRUNC(Nvl(Liberadas.Liberadas,0)/Recepcionadas.Recepcionadas * 100,2) PORCENTAGEM,
  Nvl(Devolvidas.Devolvidas,0), TRUNC(Nvl(Devolvidas.Devolvidas,0)/Recepcionadas.Recepcionadas * 100,2) PORCENTAGEM,
  Nvl(APONTAMENTOFRAUDE.APONTAMENTOFRAUDE,0),TRUNC(Nvl(APONTAMENTOFRAUDE.APONTAMENTOFRAUDE,0)/Recepcionadas.Recepcionadas * 100,2) PORCENTAGEM
FROM
--GERAL
 (SELECT Count(DISTINCT P.batch_CODE) Recepcionadas, pacoteprocessado_recebidoem dataRec, PACOTEPROCESSADO_IMPORTADOEM dataImport
 FROM proc_bk p
 INNER JOIN  batch_bk b  ON p.batch_code=b.batch_code
 INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
 GROUP BY pacoteprocessado_recebidoem,PACOTEPROCESSADO_IMPORTADOEM
 ORDER BY pacoteprocessado_recebidoem,PACOTEPROCESSADO_IMPORTADOEM) Recepcionadas

LEFT JOIN

--LIBERADAS
(SELECT
    Count( DISTINCT P.PROC_CODE) Liberadas,pacoteprocessado_recebidoem dataRec
FROM PROC_bk P
    INNER JOIN BATCH_bk  b ON P.BATCH_CODE=B.BATCH_CODE AND P.PROC_STATUS='G0' AND BATCH_STATUS='G0'
    INNER JOIN pacoteprocessado_bk pp ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE   AND PP.PACOTEPROCESSADO_STATUS=0
WHERE  p.proc_code NOT IN
  (
    SELECT proc_code FROM logproc_bk WHERE LOGPROC_ACTION IN( 'DA', 'LA' )
  )
AND p.proc_code NOT IN
  (
    SELECT proc_code FROM regraproc_bk WHERE regraproc_status = 'M'
  )
 GROUP BY pacoteprocessado_recebidoem
 ORDER BY pacoteprocessado_recebidoem) LIBERADAS

 ON Recepcionadas.dataRec = Liberadas.dataRec
 LEFT JOIN

--DEVOLVIDAS
(SELECT Count( DISTINCT P.PROC_CODE) Devolvidas,pacoteprocessado_recebidoem dataRec
FROM PACOTEPROCESSADO_BK PP
    INNER JOIN BATCH_BK B ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
    INNER JOIN PROC_BK P ON P.BATCH_CODE=B.BATCH_CODE AND P.PROC_STATUS='G0' AND BATCH_STATUS='G0'                  --P.PROC_STATUS NOT IN ('*','DE','3A') AND NOT (B.BATCH_STATUS <> P.PROC_STATUS)
    INNER JOIN REGRAPROC_BK RP ON P.PROC_CODE=RP.PROC_CODE
    INNER JOIN REGRA R ON R.REGRA_CODE=RP.REGRA_CODE
 WHERE P.PROC_DECISAO=2 OR
 (R.REGRA_FASE='C' AND R.REGRA_CLASSIF='E' AND RP.REGRAPROC_STATUS IN ('M'))
 --OR (R.REGRA_FASE='C' AND R.REGRA_CLASSIF='E' AND RP.REGRAPROC_STATUS NOT IN ('M') AND P.PROC_OBS IS NOT NULL AND PROC_DECISAO IS NULL)
 AND (Trim(B.BATCH_TFIM) IS NOT NULL OR Trim(B.BATCH_TEXPORT) IS NOT NULL)
 GROUP BY pacoteprocessado_recebidoem
 ORDER BY pacoteprocessado_recebidoem) Devolvidas

 ON Recepcionadas.dataRec = Devolvidas.dataRec
 left JOIN

 (  SELECT Count(DISTINCT P.PROC_CODE) Erro, pacoteprocessado_recebidoem dataRec
 FROM proc_bk p
 INNER JOIN  batch_bk b  ON p.batch_code=b.batch_code AND P.PROC_STATUS = B.BATCH_STATUS                 --P.PROC_STATUS IN ('*','DE','3A','5A') AND NOT (B.BATCH_STATUS <> P.PROC_STATUS)
 INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
 WHERE  BATCH_STATUS NOT IN ('G0')
 OR (Trim(B.BATCH_TFIM) IS NULL AND Trim(B.BATCH_TEXPORT) IS NULL)
 GROUP BY pacoteprocessado_recebidoem
 ORDER BY pacoteprocessado_recebidoem) Erro

 ON Erro.dataRec = Recepcionadas.dataRec
 LEFT JOIN

 (SELECT  Count(DISTINCT P.PROC_CODE) INCONSISTENTES, pacoteprocessado_recebidoem dataRec
 FROM proc_bk p
 INNER JOIN  batch_bk b  ON p.batch_code=b.batch_code AND (B.BATCH_STATUS <> P.PROC_STATUS) --P.PROC_STATUS='G0' AND BATCH_STATUS='G0'                  --NOT IN ('*','DE','3A') AND
 INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
 GROUP BY pacoteprocessado_recebidoem
 ORDER BY pacoteprocessado_recebidoem
  ) INCONSISTENTES

   ON INCONSISTENTES.dataRec = Recepcionadas.dataRec
 LEFT JOIN

 ( SELECT Count(DISTINCT P.PROC_CODE) APONTAMENTOFRAUDE, pacoteprocessado_recebidoem dataRec
    FROM PROC_BK P
      INNER JOIN MDOC_BK MD  ON P.PROC_CODE=MD.PROC_CODE AND Trim(MD.MDOC_INDICIOFRAUDE) IS NOT NULL
      INNER JOIN BATCH_BK B  ON p.batch_code=b.batch_code AND P.PROC_STATUS='G0' AND BATCH_STATUS='G0'                  --P.PROC_STATUS NOT IN ('*','DE','3A')  AND NOT (B.BATCH_STATUS <> P.PROC_STATUS)
      INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
    GROUP BY pacoteprocessado_recebidoem
    ORDER BY pacoteprocessado_recebidoem ) APONTAMENTOFRAUDE

ON Recepcionadas.dataRec = APONTAMENTOFRAUDE.dataRec
LEFT JOIN
--Sla_ok DUAS HORAS
(SELECT
      Count (DISTINCT p.proc_code) SLA_OK_2Horas,pacoteprocessado_recebidoem dataRec
FROM
    PROC_BK P
    INNER JOIN batch_BK b  ON p.batch_code=b.batch_code AND P.PROC_STATUS='G0' AND BATCH_STATUS='G0'                  --P.PROC_STATUS NOT IN ('*','DE','3A') AND NOT (B.BATCH_STATUS <> P.PROC_STATUS)
    INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
    WHERE
    (
      (
        (
          CASE WHEN Trim(BATCH_TFIM) IS NULL
          THEN BATCH_TEXPORT
          ELSE
              BATCH_TFIM
          END
         ) - pacoteprocessado_recebidoem
      ) * 24
      - (CASE WHEN BATCH_TAPROV > BATCH_TFORMAL
          THEN ABS(BATCH_TAPROV - BATCH_TFORMAL) * 24
          ELSE 0
          END
        )
    ) <2
GROUP BY pacoteprocessado_recebidoem
ORDER BY pacoteprocessado_recebidoem) SLA_OK_2Horas


ON SLA_OK_2Horas.dataRec = Recepcionadas.dataRec
LEFT JOIN


--Sla_ok até às 19:30
(SELECT
     Count (DISTINCT p.proc_code) SLA_OK_19Horas,pacoteprocessado_recebidoem dataRec
FROM
    PROC_BK P
    INNER JOIN batch_BK b  ON p.batch_code=b.batch_code AND P.PROC_STATUS='G0' AND BATCH_STATUS='G0'                  --P.PROC_STATUS NOT IN ('*','DE','3A') AND NOT (B.BATCH_STATUS <> P.PROC_STATUS)
    INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
    WHERE pacoteprocessado_recebidoem < To_Date(To_Char(pacoteprocessado_recebidoem,'dd/MM/yyyy' ) || ' 17:00','dd/MM/yyyy HH24:mi')
    AND (CASE WHEN Trim(BATCH_TFIM) IS NULL
      THEN BATCH_TEXPORT
    ELSE
        BATCH_TFIM
    END ) <  To_Date(To_Char(pacoteprocessado_recebidoem,'dd/MM/yyyy' ) || ' 19:30','dd/MM/yyyy HH24:mi')
    AND (((CASE WHEN Trim(BATCH_TFIM) IS NULL
      THEN BATCH_TEXPORT
    ELSE
        BATCH_TFIM
    END ) - pacoteprocessado_recebidoem) * 24
      - (CASE WHEN BATCH_TAPROV > BATCH_TFORMAL
          THEN ABS(BATCH_TAPROV - BATCH_TFORMAL) * 24
          ELSE 0
          END
        )
    ) >2
GROUP BY pacoteprocessado_recebidoem
ORDER BY pacoteprocessado_recebidoem) SLA_OK_19Horas

ON Recepcionadas.dataRec = SLA_OK_19Horas.dataRec
LEFT JOIN

 --Sla_OK após as 17:00
(SELECT
     Count (DISTINCT p.proc_code) SLA_OK_APOS17Horas,pacoteprocessado_recebidoem dataRec
FROM
    PROC_BK P
    INNER JOIN batch_BK b  ON p.batch_code=b.batch_code AND P.PROC_STATUS='G0' AND BATCH_STATUS='G0'                  --P.PROC_STATUS NOT IN ('*','DE','3A') AND NOT (B.BATCH_STATUS <> P.PROC_STATUS)
    INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
    WHERE
      pacoteprocessado_recebidoem >= To_Date(To_Char(pacoteprocessado_recebidoem,'dd/MM/yyyy' ) || ' 17:00','dd/MM/yyyy HH24:mi')
    AND
      (CASE WHEN Trim(BATCH_TFIM) IS NULL
       THEN BATCH_TEXPORT
        ELSE BATCH_TFIM
        END ) <  To_Date(To_Char(pacoteprocessado_recebidoem,'dd/MM/yyyy' ) || ' 19:30','dd/MM/yyyy HH24:mi')
            +  CASE To_Char(PACOTEPROCESSADO_RECEBIDOEM, 'd')
                WHEN '6' THEN  3
                ELSE   1  END
    AND (((
          CASE WHEN Trim(BATCH_TFIM) IS NULL
          THEN BATCH_TEXPORT
          ELSE BATCH_TFIM END
         ) - pacoteprocessado_recebidoem
        ) * 24
        - (CASE WHEN BATCH_TAPROV > BATCH_TFORMAL
            THEN ABS(BATCH_TAPROV - BATCH_TFORMAL) * 24
            ELSE 0  END )
      ) > 2
GROUP BY pacoteprocessado_recebidoem
ORDER BY pacoteprocessado_recebidoem) SLA_OK_APOS17Horas

ON SLA_OK_APOS17Horas.dataRec = Recepcionadas.dataRec
LEFT JOIN

-- --Sla_NOK dia anterior

(SELECT
     Count (DISTINCT p.proc_code) SLA_NOK_DIAANTERIOR,pacoteprocessado_recebidoem dataRec
FROM
    PROC_BK P
    INNER JOIN batch_BK b  ON p.batch_code=b.batch_code AND P.PROC_STATUS='G0' AND BATCH_STATUS='G0'                  --P.PROC_STATUS NOT IN ('*','DE','3A')  AND NOT (B.BATCH_STATUS <> P.PROC_STATUS)
    INNER JOIN PACOTEPROCESSADO_BK  PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
    WHERE
      pacoteprocessado_recebidoem > To_Date(To_Char(pacoteprocessado_recebidoem,'dd/MM/yyyy' ) || ' 17:00','dd/MM/yyyy HH24:mi')
    AND
      (CASE WHEN Trim(BATCH_TFIM) IS NULL
        THEN BATCH_TEXPORT
        ELSE BATCH_TFIM END
      ) >  To_Date(To_Char(pacoteprocessado_recebidoem,'dd/MM/yyyy' ) || ' 19:30','dd/MM/yyyy HH24:mi')
          +  CASE To_Char(PACOTEPROCESSADO_RECEBIDOEM, 'd')
            WHEN '6' THEN  3
            ELSE 1  END
    AND
      (((CASE WHEN Trim(BATCH_TFIM) IS NULL
          THEN BATCH_TEXPORT
          ELSE BATCH_TFIM END
        ) - pacoteprocessado_recebidoem
       ) * 24
        - (CASE WHEN BATCH_TAPROV > BATCH_TFORMAL
            THEN ABS(BATCH_TAPROV - BATCH_TFORMAL) * 24
            ELSE 0  END )
      ) > 2

GROUP BY pacoteprocessado_recebidoem
ORDER BY pacoteprocessado_recebidoem )SLA_NOK_DIAANTERIOR

ON Recepcionadas.dataRec = SLA_NOK_DIAANTERIOR.dataRec
LEFT JOIN


--Sla_NOK dia
(SELECT
      Count (DISTINCT p.proc_code) SLA_NOK_DIA,pacoteprocessado_recebidoem dataRec
FROM
    PROC_BK P
    INNER JOIN batch_BK b  ON p.batch_code=b.batch_code AND P.PROC_STATUS='G0' AND BATCH_STATUS='G0'                  --P.PROC_STATUS NOT IN ('*','DE','3A')   AND NOT (B.BATCH_STATUS <> P.PROC_STATUS)
    INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
    WHERE pacoteprocessado_recebidoem < To_Date(To_Char(pacoteprocessado_recebidoem,'dd/MM/yyyy' ) || ' 17:00','dd/MM/yyyy HH24:mi')
    AND (CASE WHEN Trim(BATCH_TFIM) IS NULL
      THEN BATCH_TEXPORT
    ELSE
        BATCH_TFIM
    END ) > To_Date(To_Char(pacoteprocessado_recebidoem,'dd/MM/yyyy' ) || ' 19:30','dd/MM/yyyy HH24:mi')
    AND (((CASE WHEN Trim(BATCH_TFIM) IS NULL
          THEN BATCH_TEXPORT
          ELSE BATCH_TFIM END
        ) - pacoteprocessado_recebidoem
       ) * 24
        - (CASE WHEN BATCH_TAPROV > BATCH_TFORMAL
            THEN ABS(BATCH_TAPROV - BATCH_TFORMAL) * 24
            ELSE 0  END )
      ) > 2

GROUP BY pacoteprocessado_recebidoem
ORDER BY pacoteprocessado_recebidoem ) SLA_NOK_DIA

ON SLA_NOK_DIA.dataRec = Recepcionadas.dataRec
left JOIN

(SELECT Count(DISTINCT P.batch_CODE) AtuacaoBanco, pacoteprocessado_recebidoem dataRec
  FROM PROC_BK P
    INNER JOIN batch_BK b  ON p.batch_code=b.batch_code AND P.PROC_STATUS='G0' AND BATCH_STATUS='G0'
    INNER JOIN PACOTEPROCESSADO_BK  PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
    WHERE P.PROC_CODE IN (SELECT PROC_CODE FROM LOGPROC_BK WHERE  LOGPROC_ACTION IN ('DA','LA'))
    AND (Trim(B.BATCH_TFIM) IS NOT NULL OR Trim(B.BATCH_TEXPORT) IS NOT NULL)
    GROUP BY pacoteprocessado_recebidoem
    ORDER BY pacoteprocessado_recebidoem) AtuacaoBanco

 ON AtuacaoBanco.dataRec = Recepcionadas.dataRec
 left JOIN

  (SELECT Count(DISTINCT B.batch_CODE) AlteracaoCadastral, pacoteprocessado_recebidoem dataRec
  FROM TDCAMPOS TDC
    INNER JOIN MDOCDADOS_BK MDD ON MDD.TDCAMPOS_CODE=TDC.TDCAMPOS_CODE
    INNER JOIN MDOC_BK MD  ON MD.MDOC_CODE=MDD.MDOC_CODE
    INNER JOIN BATCH_BK B  ON B.BATCH_CODE=MD.BATCH_CODE
    INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
    WHERE TDC.TDCAMPOS_VALIDACAO = 'S' AND  TDC.TYPEDOC_ID IN (1)
    AND Trim( MDD.MDOCDADOS_VALOR2) <> Trim(MDD.MDOCDADOS_VALOROK)
    AND B.BATCH_CODE IN (
      SELECT B.BATCH_CODE
        FROM BATCH_BK B
          INNER JOIN PROC_BK P ON p.batch_code=b.batch_code
          AND P.PROC_STATUS='G0' AND BATCH_STATUS='G0'
          AND (Trim(B.BATCH_TFIM) IS NOT NULL OR Trim(B.BATCH_TEXPORT) IS NOT NULL) )
    GROUP BY pacoteprocessado_recebidoem
    ORDER BY pacoteprocessado_recebidoem) AlteracaoCadastral

 ON AlteracaoCadastral.dataRec = Recepcionadas.dataRec
 left JOIN

 (SELECT Count(DISTINCT P.batch_CODE) AnaliseManual, pacoteprocessado_recebidoem dataRec
FROM MDOC_BK MD
    INNER JOIN PROC_bk P ON P.PROC_CODE=MD.PROC_CODE
    INNER JOIN batch_BK b  ON p.batch_code=b.batch_code AND P.PROC_STATUS='G0' AND BATCH_STATUS='G0'
    INNER JOIN PACOTEPROCESSADO_BK  PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
WHERE (Trim(B.BATCH_TFIM) IS NOT NULL OR Trim(B.BATCH_TEXPORT) IS NOT NULL)
  AND MD.MDOC_code IN (
          SELECT MD.MDOC_code
          FROM MDOc_Bk MD
            INNER JOIN LOGMDOC_BK LMD ON MD.MDOC_Code=LMD.MDOC_Code
          WHERE LOGMDOC_OBS ='Alçada diferença de digitação')
    GROUP BY pacoteprocessado_recebidoem
    ORDER BY pacoteprocessado_recebidoem ) AnaliseManual


    ON AnaliseManual.dataRec = Recepcionadas.dataRec
    left JOIN



(SELECT CASE WHEN OCRCOMPLEMENTOU.TOTAL =  RECONHECIVEL.TOTAL THEN 1
            ELSE 0
       END  ReconhecimentoAutomatico, OCRCOMPLEMENTOU.dataRec  dataRec
  FROM
    (SELECT
        Sum(MDD.MDOCDADOS_OCRCOMPLEMENTOU) TOTAL, pacoteprocessado_recebidoem dataRec
    FROM MDOCDADOS_BK MDD
    INNER JOIN MDOC_BK MD  ON MD.MDOC_CODE=MDD.MDOC_CODE
    INNER JOIN BATCH_BK B  ON B.BATCH_CODE=MD.BATCH_CODE
    INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
    WHERE MDOCDADOS_OCRCOMPLEMENTOU=1
        GROUP BY pacoteprocessado_recebidoem
    ORDER BY pacoteprocessado_recebidoem
    ) OCRCOMPLEMENTOU

    LEFT JOIN
    (SELECT Count(TDC.TDCAMPOS_CODE) TOTAL,pacoteprocessado_recebidoem dataRec
    FROM TDCAMPOS TDC
    INNER JOIN MDOCDADOS_BK MDD ON MDD.TDCAMPOS_CODE=TDC.TDCAMPOS_CODE
    INNER JOIN MDOC_BK MD  ON MD.MDOC_CODE=MDD.MDOC_CODE
    INNER JOIN BATCH_BK B  ON B.BATCH_CODE=MD.BATCH_CODE
    INNER JOIN PACOTEPROCESSADO_BK PP ON PP.PACOTEPROCESSADO_CODE=B.PACOTEPROCESSADO_CODE
    WHERE TDCAMPOS_RECONHECIVEL=1
    GROUP BY pacoteprocessado_recebidoem
    ORDER BY pacoteprocessado_recebidoem) RECONHECIVEL

    ON   RECONHECIVEL.dataRec = OCRCOMPLEMENTOU.dataRec


ORDER BY OCRCOMPLEMENTOU.dataRec) ReconhecimentoAutomatico

ON ReconhecimentoAutomatico.dataRec = Recepcionadas.dataRec

WHERE Recepcionadas.dataRec BETWEEN To_Date('01/04/2013 00:00:00','dd/MM/yyyy HH24:MI:ss') AND To_Date('30/04/2013 23:59:59','dd/MM/yyyy HH24:MI:ss')

ORDER BY Recepcionadas.dataRec
";
            return this.Session.CreateSQLQuery(string.Format(sql, dataInicial.ToString("dd/MM/yyyy"), dataFinal.ToString("dd/MM/yyyy")))
                .SetResultTransformer(CustomResultTransformer<FaturamentoPorPeriodo>.Do())
                .List<FaturamentoPorPeriodo>();
        }
    }
}
