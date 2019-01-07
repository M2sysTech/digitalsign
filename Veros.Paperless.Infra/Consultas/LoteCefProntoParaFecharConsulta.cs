namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class LoteCefProntoParaFecharConsulta : DaoBase, ILoteCefProntoParaFecharConsulta
    {
        public IList<LotecefPronto> ObterQuantidades(int lotecefId)
        {
            var sql = @"SELECT " + lotecefId.ToString() + @" LotecefId, 
(SELECT count(1) FROM batch WHERE lotecef_code = :lotecefId) QtdeTotal, 
(SELECT Count(1) FROM batch WHERE lotecef_code = :lotecefId AND batch_qualicef = 1) QtdeQualiCef 
FROM dual     
";
            return this.Session.CreateSQLQuery(sql)
               .AddScalar("LotecefId", NHibernateUtil.Int32)
               .AddScalar("QtdeTotal", NHibernateUtil.Int32)
               .AddScalar("QtdeQualiCef", NHibernateUtil.Int32)
               .SetParameter("lotecefId", lotecefId)
               .SetResultTransformer(CustomResultTransformer<LotecefPronto>.Do())
               .List<LotecefPronto>();
        }
    }
}