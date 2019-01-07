namespace Veros.Paperless.Infra.Consultas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Hibernate;
    using Model.Consultas;
    using NHibernate;

    public class RemontagemDeTermoConsulta : DaoBase, IRemontagemDeTermoConsulta
    {
        public IList<int> Obter()
        {
            var sql = @"
SELECT BATCH_CODE Id 
FROM REMONTAGEMTERMO
";
            return this.Session.CreateSQLQuery(sql)
                .AddScalar("Id", NHibernateUtil.Int32)
                .List<int>();
        }
    }
}
