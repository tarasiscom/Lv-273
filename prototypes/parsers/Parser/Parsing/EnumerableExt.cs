using System;
using System.Collections.Generic;
using System.Text;

namespace Parsing
{
    /// <summary>
    /// Borrowed from https://stackoverflow.com/a/11775295
    /// </summary>
    public static class EnumerableExt
    {
        /// <summary>
        /// Partitions the <paramref name="input"/> into the subsequences of <paramref name="blockSize"/>.
        /// </summary>
        /// <typeparam name="T">A type of a sequence <paramref name="input"/>.</typeparam>
        /// <param name="input">An IEnumerable to sequence.</param>
        /// <param name="blockSize">A size of the block to produce.</param>
        /// <returns>A collection of enumerables to iterate over.</returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> input, int blockSize)
        {
            var enumerator = input.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return NextPartition(enumerator, blockSize);
            }
        }

        private static IEnumerable<T> NextPartition<T>(IEnumerator<T> enumerator, int blockSize)
        {
            do
            {
                yield return enumerator.Current;
            }
            while (--blockSize > 0 && enumerator.MoveNext());
        }
    }
}
