namespace Veros.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ListExtension
    {
        public static bool IsEmpty<T>(this IList<T> list)
        {
            return list.Count == 0;
        }

        public static List<T[]> DividirEmLotes<T>(this IList<T> source, int lotes)
        {
            if (source.Count == 0)
            {
                return new List<T[]>();
            }

            var newList = new List<T[]>();

            var itensPorLote = Convert.ToInt32(source.Count / lotes);

            if (itensPorLote == 0)
            {
                newList.Add(source.ToArray());
                return newList;
            }

            var resto = source.Count % lotes;
            var skip = 0;

            for (int i = 0; i < lotes; i++)
            {
                if (i == lotes - 1)
                {
                    itensPorLote = itensPorLote + resto;
                }

                var lote = source.Skip(skip).Take(itensPorLote).ToArray();

                if (lote.Length == 0)
                {
                    break;
                }

                newList.Add(lote);
                skip += itensPorLote;
            }

            return newList;
        }
    }
}
