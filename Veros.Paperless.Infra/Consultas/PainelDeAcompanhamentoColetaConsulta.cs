namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class PainelDeAcompanhamentoColetaConsulta : DaoBase, IPainelDeAcompanhamentoColetaConsulta
    {
        public IList<PainelDeAcompanhamentoColeta> Obter(DateTime? dataInicio, DateTime? dataFim)
        {
            if (dataInicio.HasValue == false)
            {
                dataInicio = Convert.ToDateTime("01/01/1900");
            } 

            if (dataFim.HasValue == false)
            {
                dataFim = Convert.ToDateTime("01/01/2100");
            }

            const string Sql = @"
SELECT CL.COLETA_CODE Id, CL.COLETA_DATA Data, CL.COLETA_UF Uf, CL.COLETA_QTD1 QuantidadeEmAgendamento, 
(SELECT C.COLETA_QTD1 FROM COLETA C WHERE C.COLETA_CODE = CL.COLETA_CODE AND C.COLETA_STATUS IN ('10', '11') ) QuantidadeEmTransportadora,
(SELECT COUNT(P.PACK_CODE) FROM PACK P INNER JOIN COLETA C ON P.COLETA_CODE = C.COLETA_CODE WHERE P.COLETA_CODE = CL.COLETA_CODE AND C.COLETA_STATUS IN ('10', '12', '15') AND P.PACK_STATUS = 'R') QuantidadeEmRecepcao, 
(SELECT COUNT(P.PACK_CODE) FROM PACK P INNER JOIN COLETA C ON P.COLETA_CODE = C.COLETA_CODE WHERE P.COLETA_CODE = CL.COLETA_CODE AND C.COLETA_STATUS = '30' AND P.PACK_STATUS = 'R') QuantidadeEmConferencia,
(SELECT COUNT(DISTINCT P.PACK_CODE) FROM PACK P INNER JOIN COLETA C ON P.COLETA_CODE = C.COLETA_CODE WHERE P.COLETA_CODE = CL.COLETA_CODE AND P.PACK_CODE NOT IN (SELECT PACK_CODE FROM BATCH WHERE P.PACK_CODE = PACK_CODE) AND C.COLETA_STATUS = '30' AND P.PACK_STATUS = 'C') QuantidadeEmPreparo, 
(SELECT COUNT(0) FROM PACK P INNER JOIN OCORRENCIA O ON P.PACK_CODE = O.PACOTE_CODE WHERE P.COLETA_CODE = CL.COLETA_CODE) QuantidadeDeOcorrencias
FROM COLETA CL
LEFT JOIN PACK PC ON PC.COLETA_CODE = CL.COLETA_CODE 
WHERE CL.COLETA_STATUS IN ('10', '11', '12', '15', '30')
AND CL.COLETA_DATA BETWEEN :dataInicial AND :dataFinal
GROUP BY CL.COLETA_CODE, CL.COLETA_DATA , CL.COLETA_UF, CL.COLETA_QTD1
ORDER BY CL.COLETA_CODE, CL.COLETA_DATA, CL.COLETA_UF 
";

            return this.Session
                .CreateSQLQuery(Sql)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Data", NHibernateUtil.DateTime)
                .AddScalar("Uf", NHibernateUtil.String)
                .AddScalar("QuantidadeEmAgendamento", NHibernateUtil.Int32)
                .AddScalar("QuantidadeEmTransportadora", NHibernateUtil.Int32)
                .AddScalar("QuantidadeEmRecepcao", NHibernateUtil.Int32)
                .AddScalar("QuantidadeEmConferencia", NHibernateUtil.Int32)
                .AddScalar("QuantidadeEmPreparo", NHibernateUtil.Int32)
                .AddScalar("QuantidadeDeOcorrencias", NHibernateUtil.Int32)
                .SetDateTime("dataInicial", dataInicio.GetValueOrDefault())
                .SetDateTime("dataFinal", dataFim.GetValueOrDefault().AddDays(1))
                .SetResultTransformer(CustomResultTransformer<PainelDeAcompanhamentoColeta>.Do())
                .List<PainelDeAcompanhamentoColeta>();
        }
    }
}