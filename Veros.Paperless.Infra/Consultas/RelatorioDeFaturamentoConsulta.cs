namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.Entidades;
    using NHibernate;

    public class RelatorioDeFaturamentoConsulta : DaoBase, IRelatorioDeFaturamentoConsulta
    {
        public IList<DossieParaFaturamento> Obter(DateTime dataInicio, DateTime dataFim)
        {
            const string Sql = @"
SELECT 
    PP.PACOTEPROCESSADO_RECEBIDOEM DataPacote, 
    PK.PACK_IDENTIFICACAO Caixa, 
    C.COLETA_UF Origem, 
    P.PROC_IDENTIFICACAO Dossie, 
    PP.PACOTEPROCESSADO_FIMRECEPCAO DataPacoteAprovado,
    Count(D.DOC_CODE) QuantidadePaginas
FROM PACOTEPROCESSADO PP
  INNER JOIN BATCH B ON PP.PACOTEPROCESSADO_CODE = B.PACOTEPROCESSADO_CODE
  INNER JOIN PACK PK ON B.PACK_CODE = PK.PACK_CODE
  INNER JOIN COLETA C ON PK.COLETA_CODE = C.COLETA_CODE
  INNER JOIN PROC P ON B.BATCH_CODE = P.BATCH_CODE
  INNER JOIN MDOC M ON B.BATCH_CODE = M.BATCH_CODE AND M.TYPEDOC_ID != 27 AND M.TYPEDOC_ID != 13 
  INNER JOIN DOC D ON M.MDOC_CODE = D.MDOC_CODE AND D.DOC_STATUS != '*' AND D.DOC_FILETYPE = 'JPG'
WHERE 
    PP.PACOTEPROCESSADO_STATUS = :statusPacoteProcessado
    AND PP.PACOTEPROCESSADO_RECEBIDOEM BETWEEN :dataInicio AND :dataFim
GROUP BY PP.PACOTEPROCESSADO_RECEBIDOEM, PK.PACK_IDENTIFICACAO, C.COLETA_UF, P.PROC_IDENTIFICACAO, PP.PACOTEPROCESSADO_FIMRECEPCAO
ORDER BY PP.PACOTEPROCESSADO_RECEBIDOEM, PK.PACK_IDENTIFICACAO
";
            return this.Session.CreateSQLQuery(Sql)
               .AddScalar("DataPacote", NHibernateUtil.DateTime)
               .AddScalar("Caixa", NHibernateUtil.AnsiString)
               .AddScalar("Origem", NHibernateUtil.AnsiString)
               .AddScalar("Dossie", NHibernateUtil.AnsiString)
               .AddScalar("DataPacoteAprovado", NHibernateUtil.DateTime)
               .AddScalar("QuantidadePaginas", NHibernateUtil.Int32)
               .SetParameter("statusPacoteProcessado", StatusPacote.AprovadoNaQualidade)
               .SetParameter("dataInicio", dataInicio)
               .SetParameter("dataFim", dataFim)
               .SetResultTransformer(CustomResultTransformer<DossieParaFaturamento>.Do())
               .List<DossieParaFaturamento>();
        }
    }
}