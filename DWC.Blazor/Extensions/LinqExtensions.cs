using System;
using System.Collections.Generic;
using System.Linq;

namespace DWC.Blazor.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return Shuffle(source, new Random());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rand)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (rand == null) throw new ArgumentNullException(nameof(rand));

            T[] array = source.ToArray();
            int length = array.Length;

            for (int i = length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                T buffer = array[i];
                array[i] = array[j];
                array[j] = buffer;
            }

            for (int i = 0; i < length; i++)
            {
                yield return array[i];
            }
        }
    }
}