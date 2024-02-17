namespace NamehaveCat.Scripts.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
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

        [Pure] public static bool Any<T>(this IList<T> array, Predicate<T> predicate, int len)
        {
            for (var i = 0; i < len; i++)
                if (predicate(array[i]))
                    return true;

            return false;
        }
    }
}