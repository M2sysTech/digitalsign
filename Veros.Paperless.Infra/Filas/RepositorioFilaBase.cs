namespace Veros.Paperless.Infra.Filas
{
    using System.Collections.Generic;
    using NHibernate;
    using Veros.Data.Hibernate;
    using Veros.Framework;
    using Veros.Queues;

    public abstract class RepositorioFilaBase : DaoBase
    {
        protected IList<QueueItem> ExecuteQuery(string sql, string queueId = "")
        {
            using (var transaction = this.unitOfWork.Begin())
            {
                var sqlQuery = transaction.CurrentSession
                    .CreateSQLQuery(string.Format(sql, this.GetStartTimeFilter()))
                    .AddScalar("Id", NHibernateUtil.Int32)
                    .AddScalar("DequeuedAt", NHibernateUtil.DateTime);

                if (string.IsNullOrEmpty(queueId) == false)
                {
                    sqlQuery.SetInt32("queueId", queueId.ToInt());
                }

                return sqlQuery
                    .SetResultTransformer(CustomResultTransformer<QueueItem>.Do())
                    .List<QueueItem>();
            }           
        }

        protected virtual string GetStartTimeFilter()
        {
            return string.Empty;
        }
    }
}
