using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Talent.Core
{
    /// <summary>
    /// Represents Numeric Utilities
    /// </summary>
    public static class NumericUtil
    {
        /// <summary>
        /// Determines whether [is between inclusive] [the specified source].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>
        /// 	<c>true</c> if [is between inclusive] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBetweenInclusive(this int source, int start, int end)
        {
            return source >= start && source <= end;
        }


        public static bool IsValidIdFormat(int? id)
        {
            if (id == null || id == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Zeroes the one to bool.
        /// </summary>
        /// <param name="zeroOrOne">The zero or one.</param>
        /// <returns></returns>
        public static bool ZeroOneToBool(this int zeroOrOne)
        {
            return zeroOrOne == 1;
        }

        /// <summary>
        /// Empties the array.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <returns></returns>
        public static TType[] EmptyArray<TType>()
        {
            return new TType[] { };
        }

        public static string ToMoney(this decimal amount, string currencySymol = null)
        {
            if (string.IsNullOrEmpty(currencySymol))
            {
                return string.Format("{0:C}", amount);
            }

            return string.Format("{0:C} {1}", amount, currencySymol);
        }
    }
}
