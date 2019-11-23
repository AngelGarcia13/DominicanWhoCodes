using System;
using System.Collections.Generic;
using System.Linq;

namespace DWC.Blazor.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>Returns a shuffled sequence from the original sequence.</summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/></typeparam>
        /// <param name="source">The sequence to shuffle.</param>
        /// <returns>A new shuffled sequence of <paramref name="source"/></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return Shuffle(source, new Random());
        }

        /// <summary>Returns a shuffled sequence from the original sequence.</summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/></typeparam>
        /// <param name="source">The sequence to shuffle.</param>
        /// <param name="rand">A random generator used as part of the selection algorithm.</param>
        /// <returns>A new shuffled sequence of <paramref name="source"/></returns>
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