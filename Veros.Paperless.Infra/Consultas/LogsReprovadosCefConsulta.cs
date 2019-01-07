namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Consultas;
    using Model.ViewModel;
    using NHibernate;

    public class LogsReprovadosCefConsulta : DaoBase, ILogsReprovadosCefConsulta
    {
        public IList<LogDeReprovacaoCefViewModel> Obter(int lotecefId)
        {
            const string Sql = @"
SELECT 
    L.LOG_MODULE Tipo,
    U.USR_NAME Usuario,
    L.LOG_DATETIME Data,
    L.LOG_DESC Observacao,
    L.LOG_ACTION Acao
FROM 
    Log L
    INNER JOIN USR U ON U.USR_MATRICULA = L.USR_NAME
WHERE
    L.LOG_REGISTER = :lotecefId
ORDER BY Data
";

            return this.Session
            .CreateSQLQuery(Sql)
            .AddScalar("Tipo", NHibernateUtil.String)
            .AddScalar("Usuario", NHibernateUtil.String)
            .AddScalar("Data", NHibernateUtil.DateTime)
            .AddScalar("Observacao", NHibernateUtil.String)
            .AddScalar("Acao", NHibernateUtil.String)
            .SetParameter("lotecefId", lotecefId)
            .SetResultTransformer(CustomResultTransformer<LogDeReprovacaoCefViewModel>.Do())
            .List<LogDeReprovacaoCefViewModel>();
        }
    }
}
