namespace Veros.Paperless.Infra.Consultas
{
    using Data.Hibernate;
    using Model.Consultas;
    using System;
    using System.Collections.Generic;

    public class CamposReconhecidosPorDocumentoECampoConsulta : DaoBase, ICamposReconhecidosPorDocumentoECampoConsulta
    {
        public IList<CamposReconhecidosPorDocumentoECampo> Obter(DateTime dataInicio, DateTime dataFinal)
        {
            var sql = @"
                        SELECT
                          TD.TYPEDOC_DESC TipoDocumento, TC.TDCAMPOS_DESC Campo,  
                          Sum (MDD.MDOCDADOS_OCRCOMPLEMENTOU) Acertos,  
                          Sum (CASE MDD.MDOCDADOS_OCRCOMPLEMENTOU WHEN 0 THEN 1 ELSE 0 END) Erros, 
                          Count(0) Total
                        FROM TDCAMPOS TC
                        INNER JOIN MDOCDADOS MDD ON TC.TDCAMPOS_CODE = MDD.TDCAMPOS_CODE
                        INNER JOIN MDOC MD ON MDD.MDOC_CODE = MD.MDOC_CODE
                        INNER JOIN BATCH B ON MD.BATCH_CODE = B.BATCH_CODE
                        INNER JOIN TYPEDOC TD ON MD.TYPEDOC_ID = TD.TYPEDOC_ID
                        WHERE TC.TDCAMPOS_RECONHECIVEL = 1
                        AND B.BATCH_TICR BETWEEN :dataInicio AND :dataFinal
                        GROUP BY TD.TYPEDOC_DESC, TC.TDCAMPOS_DESC
                        ORDER BY TD.TYPEDOC_DESC, TC.TDCAMPOS_DESC
                    ";

            return this.Session.CreateSQLQuery(sql)
                .SetDateTime("dataInicio", dataInicio)
                .SetDateTime("dataFinal", dataFinal)
                .SetResultTransformer(CustomResultTransformer<CamposReconhecidosPorDocumentoECampo>.Do())
                .List<CamposReconhecidosPorDocumentoECampo>();
        }
    }
}
