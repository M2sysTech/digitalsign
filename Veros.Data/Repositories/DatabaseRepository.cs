namespace Veros.Data.Repositories
{
    using System;
    using Hibernate;

    public class DatabaseRepository : DaoBase, IDatabaseRepository
    {
        public DateTime GetDateTime()
        {
            return this.unitOfWork.Current
                .CurrentSession
                .CreateSQLQuery(Database.Provider.GetDateTimeQuery())
                .UniqueResult<DateTime>();
        }
    }
}