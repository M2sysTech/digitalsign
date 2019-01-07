namespace Veros.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extensões para Enumerable. 
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary> 
        /// Para cada item no enumerable executa uma ação
        /// </summary>
        /// <typeparam name="T">Tipo do enumerable</typeparam>
        /// <param name="enumerable">O enumerable</param>
        /// <param name="action">Ação a ser executada</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static string Join<T>(this IEnumerable<T> enumerable, string split, Func<T, string> action)
        {
            return enumerable
                .Aggregate(string.Empty, (current, item) => current + (action(item) + split))
                .TrimEnd(split.ToCharArray());
        }

        public static string Join<T>(this IEnumerable<T> enumerable, string split)
        {
            return enumerable.Join(split, x => x.ToString());
        }
    }
}