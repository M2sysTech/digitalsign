namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.ViewModel;
    using NHibernate;

    public class CaixasParaDevolucaoConsulta : DaoBase, ICaixasParaDevolucaoConsulta
    {
        public IList<CaixaParaDevolucaoViewModel> Obter(DateTime? diaDevolucao)
        {
            if (diaDevolucao.HasValue == false)
            {
                diaDevolucao = DateTime.Today;
            }

            /*AND PA.PACK_STATUS = 'X'*/

            const string Sql = @"
SELECT PA.PACK_IDENTIFICACAO Identificacao, Count(B.BATCH_CODE) QuantidadeDeDossies, SUM(M.MDOC_QTDEPAG) QuantidadeDePaginas
FROM PACK PA 
INNER JOIN BATCH B ON PA.PACK_CODE = B.PACK_CODE
LEFT JOIN MDOC M ON B.BATCH_CODE = M.BATCH_CODE 
WHERE M.MDOC_STATUS != '*'
AND M.TYPEDOC_ID NOT IN (75,121)
AND PA.PACK_DTDEVOLUCAO = :DtDevolucao
GROUP BY PA.PACK_IDENTIFICACAO";

            return this.Session.CreateSQLQuery(Sql).AddScalar("Identificacao", NHibernateUtil.String)
                .AddScalar("QuantidadeDeDossies", NHibernateUtil.Int32)
                .AddScalar("QuantidadeDePaginas", NHibernateUtil.Int32)
                .SetParameter("DtDevolucao", diaDevolucao)
                .SetResultTransformer(CustomResultTransformer<CaixaParaDevolucaoViewModel>.Do())
                .List<CaixaParaDevolucaoViewModel>();
        }

        IList<dynamic> ICaixasParaDevolucaoConsulta.Obter(DateTime? diaDevolucao)
        {
            throw new NotImplementedException();
        }
    }
}
