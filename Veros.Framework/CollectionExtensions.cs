//-----------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System.Collections.Generic;

    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                collection.Add(item);
            }
        }
    }
}
