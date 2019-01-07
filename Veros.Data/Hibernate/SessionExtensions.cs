namespace Veros.Data.Hibernate
{
    using Framework;
    using Framework.Modelo;
    using NHibernate;

    public static class SessionExtensions
    {
        public static void DeleteFrom(this ISession session, string tableName)
        {
            session.CreateSQLQuery("delete from " + tableName).ExecuteUpdate();
        }

        public static void DeleteAll<T>(this ISession session) where T : IEntidade
        {
            session.DeleteFrom(GetTableName<T>());
        }

        private static string GetTableName<T>()
        {
            var classMaping = HibernateConfiguration.BuiltConfiguration.GetClassMapping(typeof(T));

            if (classMaping == null)
            {
                Log.Application.InfoFormat(
                    "Não foi possível obter nome da tabela através do mapeamento do Hibernate. Verifique se a entidade {0} está mapeada",
                    typeof(T).FullName);

                return string.Empty;
            }

            return classMaping.Table.Name;
        }
    }
}
