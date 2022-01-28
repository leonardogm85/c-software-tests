using System;
using System.Collections.Generic;

namespace NerdStore.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> itens, Action<T> itemActiom)
        {
            foreach (var item in itens)
            {
                itemActiom(item);
            }
        }
    }
}
