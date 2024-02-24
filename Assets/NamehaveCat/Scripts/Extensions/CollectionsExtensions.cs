namespace NamehaveCat.Scripts.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    public static class CollectionsExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action, params T[] except)
        {
            foreach (var item in enumerable)
                if (!except.Contains(item))
                    action(item);
        }

        public static void ForEach<T>(this T[] array, Action<T, int> action)
        {
            for (var index = 0; index < array.Length; index++)
            {
                var item = array[index];
                action(item, index);
            }
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