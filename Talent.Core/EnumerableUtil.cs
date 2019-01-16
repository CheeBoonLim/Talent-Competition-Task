using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Talent.Core
{
    /// <summary>
    /// Represents Enumerable, IEnumerable Utilities
    /// </summary>
    public static class EnumerableUtil
    {
        #region IEnumerable

        /// <summary>
        /// Returns true if the generic <see cref="IEnumerable"/> object is null or contains 0 element,
        /// otherwise false.
        /// </summary>
        /// <typeparam name="T">The Type</typeparam>
        /// <param name="source">A generic <see cref="IEnumerable"/> object.</param>
        /// <returns>
        /// True if the generic <see cref="IEnumerable"/> object is null or empty.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.Count() == default(int);
        }

        /// <summary>
        /// Returns true if the  <see cref="IEnumerable"/> object is null or contains 0 element, 
        /// otherwise false.
        /// </summary>
        /// <param name="source"><see cref="IEnumerable"/> object.</param>
        /// <returns>True if the <see cref="IEnumerable"/> object is null or empty.</returns>
        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            // it is null.
            if (source == null)
            {
                return true;
            }

            // check if there is any element. returns false if any as not empty.
            IEnumerator enumerator = source.GetEnumerator();

            return !enumerator.MoveNext();
        }

        /// <summary>
        /// Returns <see cref="System.String"/> that represents current the source object, the elements within source sequence are
        /// separated by the separator.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="source">A generic <see cref="IEnumerable"/> object.</param>
        /// <param name="separator">Separator as delimiter of the sequence.</param>
        /// <returns>
        /// Returns <see cref="System.String"/> that represents current object.
        /// </returns>
        public static string ToString<T>(this IEnumerable<T> source, string separator)
        {
            if (source.IsNullOrEmpty())
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(separator))
            {
                throw new ArgumentException("Separator must be defined.");
            }

            return string.Join(separator, source.Where(n => n != null)
                                                .Select(s => s.ToString()).ToArray());
        }

        /// <summary>
        /// Returns <see cref="System.String"/> that represents current the source object, the elements within source sequence are
        /// separated by the separator.
        /// </summary>
        /// <remarks>
        /// This is the overloading of the generic extension method.
        /// </remarks>
        /// <param name="source">The source.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>The object of <see cref="String"/>
        /// </returns>
        /// 30/09/2009
        public static string ToString(this IEnumerable source, string separator)
        {
            if (source.IsNullOrEmpty())
            {
                return string.Empty;
            }

            if (separator == null)
            {
                throw new ArgumentException("separator must be defined.");
            }

            return string.Join(separator, source.Cast<object>()
                                                .Where(n => n != null)
                                                .Select(s => s.ToString()).ToArray());
        }

        /// <summary>
        /// Returns the element that has the min value indicated by the selector.
        /// </summary>
        /// <typeparam name="T">The source element type</typeparam>
        /// <typeparam name="K">The type of the return value of the selector.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The object of <see cref="T"/>
        /// </returns>
        /// 29/10/2009
        public static T MinBy<T, K>(this IEnumerable<T> source, Func<T, K> selector)
        {
            return source.MinBy(selector, Comparer<K>.Default);
        }

        /// <summary>
        /// Returns the element that has the min value indicated by the selector.
        /// </summary>
        /// <typeparam name="T">The source element type</typeparam>
        /// <typeparam name="K">The type of the return value of the selector.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>The object of <see cref="T"/>
        /// </returns>
        /// 29/10/2009
        public static T MinBy<T, K>(this IEnumerable<T> source, Func<T, K> selector,
                                        IComparer<K> comparer)
        {
            if (source.IsNullOrEmpty())
            {
                throw new ArgumentException("Source must be defined.", "source");
            }

            if (comparer == null)
            {
                throw new ArgumentException("comparer must be defined.", "comparer");
            }

            using (var iterator = source.GetEnumerator())
            {
                // move the cursor to be the first element
                iterator.MoveNext();

                // initialise the min
                T min = iterator.Current;
                K minValue = selector(min);

                while (iterator.MoveNext())
                {
                    T temp = iterator.Current;
                    K tempKey = selector(temp);

                    if (comparer.Compare(tempKey, minValue) < 0)
                    {
                        min = temp;
                        minValue = tempKey;
                    }
                }

                return min;
            }
        }

        /// <summary>
        /// Returns the element that has the max value indicated by the selector.
        /// </summary>
        /// <typeparam name="T">The source element type</typeparam>
        /// <typeparam name="K">The type of the return value of the selector.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The object of <see cref="T"/>
        /// </returns>
        /// 29/10/2009
        public static T MaxBy<T, K>(this IEnumerable<T> source, Func<T, K> selector)
        {
            return source.MaxBy(selector, Comparer<K>.Default);
        }

        /// <summary>
        /// Returns the element that has the max value indicated by the selector.
        /// </summary>
        /// <typeparam name="T">The source element type</typeparam>
        /// <typeparam name="K">The type of the return value of the selector.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>The object of <see cref="T"/>
        /// </returns>
        /// 29/10/2009
        public static T MaxBy<T, K>(this IEnumerable<T> source, Func<T, K> selector, IComparer<K> comparer)
        {
            if (source.IsNullOrEmpty())
            {
                throw new ArgumentException("Source must be defined.", "source");
            }

            if (comparer == null)
            {
                throw new ArgumentException("comparer must be defined.", "comparer");
            }

            using (var iterator = source.GetEnumerator())
            {
                // move the cursor to be the first element
                iterator.MoveNext();

                // initialise the min
                T max = iterator.Current;
                K maxValue = selector(max);

                while (iterator.MoveNext())
                {
                    T temp = iterator.Current;
                    K tempKey = selector(temp);

                    if (comparer.Compare(tempKey, maxValue) > 0)
                    {
                        max = temp;
                        maxValue = tempKey;
                    }
                }

                return max;
            }
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        /// 16/11/2009
        public static void ForEach(this IEnumerable source, Action<object> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Splits the specified STR.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <returns></returns>
        public static IEnumerable<string> Split(this string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        /// <summary>
        /// Arrayses the equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a1">The a1.</param>
        /// <param name="a2">The a2.</param>
        /// <returns></returns>
        public static bool ArraysEqual<T>(IList<T> a1, IList<T> a2)
        {
            if (ReferenceEquals(a1, a2))
            {
                return true;
            }

            if (a1 == null || a2 == null)
            {
                return false;
            }

            if (a1.Count() != a2.Count())
            {
                return false;
            }

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Count(); i++)
            {
                if (!comparer.Equals(a1[i], a2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
