//-----------------------------------------------------------------------
// <copyright file="TransformationProviderExtensions.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data
{
    using Veros.Framework;
    using Migrator.Framework;

    public static class TransformationProviderExtensions
    {
        public static void CreateSequence(
            this ITransformationProvider database, 
            string sequenceName)
        {
            database["Oracle"].ExecuteNonQuery(string.Format("CREATE SEQUENCE {0}", sequenceName));
        }

        public static void RemoveSequence(
            this ITransformationProvider database, 
            string sequenceName)
        {
            database["Oracle"].ExecuteNonQuery(string.Format("DROP SEQUENCE {0}", sequenceName));
        }

        public static void RemoveTableIfExists(
            this ITransformationProvider transformation,
            string table)
        {
            if (transformation.TableExists(table))
            {
                transformation.RemoveTable(table);
            }
        }

        public static string True(this ITransformationProvider transformation)
        {
            return IoC.Current.Resolve<IDatabaseProvider>().True();
        }

        public static string False(this ITransformationProvider transformation)
        {
            return IoC.Current.Resolve<IDatabaseProvider>().False();
        }

        public static void AddIndex(
            this ITransformationProvider transformation,
            string tableName,
            params string[] columns)
        {
            transformation.AddIndex(tableName, false, columns);
        }

        public static void AddIndex(
            this ITransformationProvider transformation,
            string tableName,
            bool unique,
            params string[] columns)
        {
            if (columns.Length == 0)
            {
                return;
            }

            var indexName = GetIndexName(tableName, columns);
            string uniqueString = unique ? "UNIQUE" : string.Empty;

            string sql = string.Format(
                "CREATE {0} INDEX {1} ON {2} ({3})", 
                uniqueString, 
                indexName, 
                tableName, 
                columns.Join(", ", s => s));

            transformation.ExecuteNonQuery(sql);
        }

        public static void RemoveIndex(
            this ITransformationProvider transformation,
            string tableName,
            params string[] columns)
        {
            var indexName = GetIndexName(tableName, columns);
            var sql = GetRemoveIndexSql(tableName, indexName);
            transformation.ExecuteNonQuery(sql);
        }

        private static string GetRemoveIndexSql(string tableName, string indexName)
        {
            if (Database.ProviderName.Equals("Postgre"))
            {
                return string.Format("DROP INDEX {0}", indexName);
            }

            return string.Format("DROP INDEX {0} ON {1}", indexName, tableName);
        }

        private static string GetIndexName(
            string tableName,
            params string[] columns)
        {
            return string.Format("{0}_{1}", tableName, columns.Join("_"));
        }
    }
}
