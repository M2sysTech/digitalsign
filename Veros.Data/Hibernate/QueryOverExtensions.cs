//-----------------------------------------------------------------------
// <copyright file="QueryOverExtensions.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Hibernate
{
    using Framework;
    using NHibernate;
    using NHibernate.Criterion.Lambda;

    public static class QueryOverExtensions
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

        public static IQueryOver<TRoot, TSubType> IsIn<TRoot, TSubType>(
            this IQueryOverRestrictionBuilder<TRoot, TSubType> builder, params object[] objects)
        {
            return builder.IsIn(objects);
        }
        
        public static IQueryOver<T> Paged<T>(this IQueryOver<T, T> query, int first, int length)
        {
            if (first > 0)
            {
                query.Skip(first);
            }

            if (length > 0)
            {
                query.Take(length);
            }

            return query;
        }
    }
}
