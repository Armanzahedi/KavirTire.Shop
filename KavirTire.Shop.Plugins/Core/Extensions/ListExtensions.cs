using System;
using System.Collections.Generic;
using System.Linq;

namespace KavirTire.Shop.Plugins.Core.Extensions
{
    public static class ListExtensions
    {
        public static List<List<T>> SplitArray<T>(this List<T> array, int count)
        {
            if (count <= 0)
                throw new Exception("Split count should be greater than zero");

            count = array.Count > count ? count : array.Count;

            var splitedArr = new List<List<T>>();
            if (array.Count == 0)
                return splitedArr;

            int arrayNum = (array.Count + count - 1) / count;
            for (int i = 0; i < arrayNum; i++)
            {
                splitedArr.Add(new List<T>(array.Skip(i * count).Take(count)));
            }
            return splitedArr;
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}