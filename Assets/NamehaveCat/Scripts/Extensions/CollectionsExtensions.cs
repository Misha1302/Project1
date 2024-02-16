namespace NamehaveCat.Scripts.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CollectionsExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action, params T[] except)
            where T : class
        {
            foreach (var item in enumerable)
                if (!except.Contains(item))
                    action(item);
        }
    }
}