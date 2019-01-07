namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.ViewModel;
    using NHibernate;

    public class RegrasReprovadasCefConsulta : DaoBase, IRegrasReprovadasCefConsulta
    {
        public IList<RegraVioladaReprovacaoCefViewModel> Obter(int lotecefId)
        {
            const string Sql = @"
SELECT 
    P.PROC_IDENTIFICACAO Dossie, M.MDOC_ORDEM Ordem, 
    T.TYPEDOC_DESC TipoDocumento,  
    R.REGRAPROC_OBS ErroApontado, R.REGRAPROC_PAGINA Pagina,
    B.BATCH_QUALICEF Amostra
FROM BATCH B JOIN PROC P ON B.BATCH_CODE = P.BATCH_CODE
    JOIN REGRAPROC R ON R.PROC_CODE = P.PROC_CODE AND R.REGRA_CODE = 2
    JOIN MDOC M ON M.MDOC_CODE = R.MDOC_CODE   
    JOIN TYPEDOC T ON T.TYPEDOC_ID = M.TYPEDOC_ID  
WHERE
    B.LOTECEF_CODE = :lotecefId
ORDER BY P.BATCH_CODE, M.MDOC_ORDEM, M.MDOC_CODE
";

            return this.Session
            .CreateSQLQuery(Sql)
            .AddScalar("Dossie", NHibernateUtil.String)
            .AddScalar("Ordem", NHibernateUtil.String)
            .AddScalar("TipoDocumento", NHibernateUtil.String)
            .AddScalar("ErroApontado", NHibernateUtil.String)
            .AddScalar("Pagina", NHibernateUtil.String)
            .AddScalar("Amostra", NHibernateUtil.Int32)
            .SetParameter("lotecefId", lotecefId)
            .SetResultTransformer(CustomResultTransformer<RegraVioladaReprovacaoCefViewModel>.Do())
            .List<RegraVioladaReprovacaoCefViewModel>();
        }
    }
}
