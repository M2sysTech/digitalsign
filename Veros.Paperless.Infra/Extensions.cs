namespace Veros.Paperless.Infra
{
    using System;
    using System.IO;
    using Framework.IO;
    using Migrator.Framework;
    using NHibernate;
    using NHibernate.Criterion.Lambda;
    using Model.FrameworkLocal;

    /// <summary>
    /// TODO: framework
    /// </summary>
    public static class Extensions
    {
        public static IQueryOver<TRoot, TSubType> In<TRoot, TSubType, TPropertyType>(
            this IQueryOverRestrictionBuilder<TRoot, TSubType> builder, params TPropertyType[] objects)
        {
            return builder.IsIn(objects);
        }

        public static IQueryOver<T> Paginado<T>(
            this IQueryOver<T, T> query, int page, int itemsPerPage)
        {
            return query
                .Skip(page.PaginateSkip(itemsPerPage))
                .Take(itemsPerPage);
        }

        public static void SetColumnAsChar4000(
            this ITransformationProvider transformationProvider,
            string tableName,
            string columnName)
        {
            string sql = string.Format(
                @"ALTER TABLE {0} MODIFY ({1} VARCHAR2(4000))",
                tableName,
                columnName);

            transformationProvider.ExecuteNonQuery(sql);
        }
    }
}
