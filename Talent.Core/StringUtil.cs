using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Talent.Core
{
    /// <summary>
    /// Represents string utilities
    /// </summary>
    /// 6/07/2009
    public static class StringUtil
    {

        /// <summary>
        /// Determines whether [is null or empty] of [the specified source].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>True if it is null or empty</returns>
        /// 6/05/2009
        public static bool IsNullOrNothing(this string source)
        {
            if (source != null)
            {
                source = source.Trim();
            }

            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// To the upper first letter.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The object of <see cref="String"/>; Returns string.Empty if source is null.
        /// </returns>
        /// 22/07/2009
        public static string ToUpperFirstLetter(this string source)
        {
            // return string.Empty if undefined
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            // convert to char array of the string
            char[] letters = source.ToCharArray();

            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);

            // return the array made of the new char array
            return new string(letters);
        }

        /// <summary>
        /// Surrounds all.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="front">The front.</param>
        /// <param name="end">The end.</param>
        /// <returns>The object of <see cref="String"/>
        /// </returns>
        /// 12/08/2009
        public static string SurroundAll(this string source, string front, string end)
        {
            return SurroundMatches(source, source, front, end, true);
        }

        /// <summary>
        /// Surrounds the matches.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="keyword">The keyword.</param>
        /// <param name="front">The front.</param>
        /// <param name="end">The end.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>The object of <see cref="String"/>
        /// </returns>
        /// 24/07/2009
        public static string SurroundMatches(this string source, string keyword, string front, string end, bool ignoreCase)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "source can not be null.");
            }

            if (source.Length == 0)
            {
                return source;
            }

            RegexOptions option = ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
            MatchCollection matches = Regex.Matches(source, keyword, option);

            for (int i = matches.Count - 1; i >= 0; i--)
            {
                Match match = matches[i];
                source = source.Insert(match.Index + match.Length, end);
                source = source.Insert(match.Index, front);
            }

            return source;
        }

        /// <summary>
        /// Takes the left of the string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="numberOfLetters">The number of letters.</param>
        /// <param name="trailing">The trailing.</param>
        /// <returns>A substring starting from index 0 to numberOfLetters concated with trailing if defined.</returns>
        public static string TakeLeft(this string source, int numberOfLetters, string trailing)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "source can not be null.");
            }

            if (numberOfLetters < 1)
            {
                throw new ArgumentException("numberOfLetters must be greater than zero.");
            }

            if (source.Length <= numberOfLetters)
            {
                return source + trailing;
            }

            return source.Substring(0, numberOfLetters)
                + (trailing.IsNullOrEmpty() ? string.Empty : trailing);
        }

        /// <summary>
        /// Truncates the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <returns></returns>
        public static string Truncate(this string s, int maxLength)
        {
            if (s.IsNullOrEmpty()) return s;
            return (s.Length > maxLength) ? s.Remove(maxLength) : s;
        }

        /// <summary>
        /// Determines whether the specified s has value.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        /// 	<c>true</c> if the specified s has value; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasValue(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Cleans the string by removing the chars defined.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="charsToRemove">The chars to remove.</param>
        /// <returns>The object of <see cref="String"/> that has been cleaned.
        /// </returns>
        /// 12/08/2009
        public static string CleanString(this string source, params char[] charsToRemove)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "source can not be null.");
            }

            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            foreach (char c in charsToRemove)
            {
                int index = source.IndexOf(c);

                if (index != -1)
                {
                    source = source.Remove(index, 1);
                }
            }

            return source;
        }

        /// <summary>
        /// Parses to int.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>int value of the string if it contains integer;otherwise default(int)</returns>
        public static int ParseToInt(this string source)
        {
            if (source.IsNullOrEmpty())
                return default(int);

            int value;
            int.TryParse(source, out value);

            return value;
        }


        /// <summary>
        /// Parses to int?.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>int value of the string if it contains integer;otherwise default(int)</returns>
        public static int? ParseToIntAsNullable(this string source)
        {
            if (source.IsNullOrEmpty())
                return null;

            int value;
            if (int.TryParse(source, out value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Parses to int or default.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="default">The default value if the parseing failed.</param>
        /// <returns>The object of <see cref="Int32"/>
        /// </returns>
        /// 12/08/2009
        public static int ParseToIntOrDefault(this string source, int @default)
        {
            if (source.IsNullOrEmpty())
            {
                return @default;
            }

            int value;
            if (!int.TryParse(source, out value))
            {
                value = @default;
            }

            return value;
        }

        /// <summary>
        /// Parses to long.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The object of <see cref="Int64"/>
        /// </returns>
        /// 12/08/2009
        public static long ParseToLong(this string source)
        {
            if (source.IsNullOrEmpty())
            {
                return default(long);
            }

            long value;
            long.TryParse(source, out value);

            return value;
        }

        /// <summary>
        /// Parses to long or default.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="default">The default value.</param>
        /// <returns>The object of <see cref="Int64"/>
        /// </returns>
        /// 12/08/2009
        public static long ParseToLongOrDefault(this string source, long @default)
        {
            if (source.IsNullOrEmpty())
            {
                return @default;
            }

            long value;
            if (!long.TryParse(source, out value))
            {
                value = @default;
            }

            return value;
        }

        /// <summary>
        /// Parses to decimal.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The object of <see cref="Decimal"/>
        /// </returns>
        /// 12/08/2009
        public static decimal ParseToDecimal(this string source)
        {
            if (source.IsNullOrEmpty())
            {
                return default(decimal);
            }

            decimal value;
            decimal.TryParse(source, out value);

            return value;
        }

        /// <summary>
        /// Parses to decimal or default.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="default">The default value.</param>
        /// <returns>The object of <see cref="Decimal"/>
        /// </returns>
        /// 12/08/2009
        public static decimal ParseToDecimalOrDefault(this string source, decimal @default)
        {
            if (source.IsNullOrEmpty())
            {
                return @default;
            }

            decimal value;
            if (!decimal.TryParse(source, out value))
            {
                value = @default;
            }

            return value;
        }

        /// <summary>
        /// Parses to double.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The object of <see cref="Double"/>
        /// </returns>
        /// 12/08/2009
        public static double ParseToDouble(this string source)
        {
            if (source.IsNullOrEmpty())
            {
                return default(double);
            }

            double value;
            double.TryParse(source, out value);

            return value;
        }

        /// <summary>
        /// Parses to double or default.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="default">The @default.</param>
        /// <returns>The object of <see cref="Double"/>
        /// </returns>
        /// 12/08/2009
        public static double ParseToDoubleOrDefault(this string source, double @default)
        {
            if (source.IsNullOrEmpty())
                return @default;

            double value;

            if (!double.TryParse(source, out value))
            {
                value = @default;
            }

            return value;
        }

        /// <summary>
        /// Parses to GUID.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="System.FormatException">
        /// The format of source is invalid.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// The format of source is invalid.
        /// </exception>
        public static Guid ParseToGuid(this string source)
        {
            if (source.IsNullOrEmpty())
            {
                return default(Guid);
            }

            return new Guid(source);
        }

        /// <summary>
        /// Parses to GUID or default.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="default">The @default.</param>
        /// <returns>The object of <see cref="Guid"/>
        /// </returns>
        /// 27/04/2009
        public static Guid ParseToGuidOrDefault(this string source, Guid @default)
        {
            if (source.IsNullOrEmpty())
            {
                return @default;
            }

            try
            {
                return new Guid(source);
            }
            catch (Exception x)
            {
                Debug.WriteLine(string.Format("{0} is invalid guid string.{1}", source, x.Message));
            }

            return @default;
        }

        /// <summary>
        /// Gets the value or null.
        /// </summary>
        /// <typeparam name="T">The type will determine if it holds the default value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The object of <see cref="Nullable&lt;T&gt;"/>
        /// </returns>
        /// 24/07/2009
        public static T? GetValueOrNull<T>(this T value) where T : struct
        {
            if (value.Equals(default(T)))
            {
                return new T?();
            }

            return value;
        }

        /// <summary>
        /// Determines whether [is default value] [the specified value].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is default value] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        /// 11/06/2009
        public static bool IsDefaultValue<T>(this T value) where T : struct
        {
            return value.Equals(default(T));
        }

        /// <summary>
        /// Email Regex Pattern
        /// </summary>
        public const string EmailRegexPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        /// <summary>
        /// Determines whether the specified email is email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// 	<c>true</c> if the specified email is email; otherwise, <c>false</c>.
        /// </returns>
        /// 11/06/2009
        public static bool IsEmail(this string email)
        {
            return email.IsNullOrNothing() ? false : Regex.IsMatch(email, EmailRegexPattern);
        }

        public static string FindFirstEmail(this IEnumerable<XElement> elements)
        {
            foreach (var item in elements)
            {
                if (item.Value.IsEmail())
                {
                    return item.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Combines the path.
        /// </summary>
        /// <param name="path1">The path1.</param>
        /// <param name="path2">The path2.</param>
        /// <returns>Combined path</returns>
        public static string CombinePath(this string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        /// <summary>
        /// Combines the URL.
        /// </summary>
        /// <param name="hostUrl">The host URL.</param>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <returns></returns>
        public static string CombineUrl(this string hostUrl, string relativeUrl)
        {
            var uri = new Uri(new Uri(hostUrl), relativeUrl);
            return uri.ToString();
        }

        /// <summary>
        /// Wraps the string(even null) to XML.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>An XElement for the xml text</returns>
        /// <remarks>
        /// If text is null, empty or invalid XML, the Blob tag will wrap the content of the text
        /// and return the Blob as the XElement object.
        /// </remarks>
        public static XElement WrapToXmlSafely(this string text)
        {
            if (text == null)
            {
                return XElement.Parse("<Blob>(null)</Blob>");
            }

            if (text.IsNullOrNothing())
            {
                return XElement.Parse("<Blob></Blob>");
            }

            try
            {
                return XElement.Parse(text);
            }
            catch
            {
                return XElement.Parse("<Blob>" + text + "</Blob>");
            }
        }

        /// <summary>
        /// Return value or empty if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src">The SRC.</param>
        /// <returns></returns>
        public static string ValueOrEmpty<T>(this Nullable<T> src) where T : struct
        {
            if (src == null)
            {
                return string.Empty;
            }

            return src.Value.ToString();
        }

        /// <summary>
        /// Parses to DateTime?.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>DateTime value of the string if it contains DateTime;otherwise null</returns>
        public static DateTime? ParseToDateTimeAsNullable(this string source)
        {
            if (source.IsNullOrEmpty())
                return null;

            DateTime value;
            if (DateTime.TryParse(source, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Parses the car jam date to date time as nullable.
        /// </summary>
        /// <param name="unixTimeStampStr">The unix time stamp STR.</param>
        /// <returns></returns>
        public static DateTime? ParseCarJamDateToDateTimeAsNullable(this string unixTimeStampStr)
        {
            double value;
            if (double.TryParse(unixTimeStampStr, out value))
            {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                dtDateTime = dtDateTime.AddSeconds(value).ToLocalTime();

                return dtDateTime;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Parses to bool?.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>bool value of the string if it contains bool;otherwise null</returns>
        public static bool? ParseToBooleanAsNullable(this string source)
        {
            if (source.IsNullOrEmpty())
                return null;

            bool value;
            if (bool.TryParse(source, out value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Toes the bit string.
        /// </summary>
        /// <param name="bits">The bits.</param>
        /// <returns></returns>
        public static string ToBitString(this BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Binaries the string to bytes.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static byte[] BinaryStringToBytes(this string input)
        {
            int numOfBytes = input.Length / 8;
            var bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
            }

            return bytes;
        }

        /// <summary>
        /// Bools to yes no.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string BoolToYesNo(this bool source, string textToAppend = null)
        {
            if (textToAppend == null)
                textToAppend = string.Empty;
            else
                textToAppend = " " + textToAppend;

            if (source)
            {
                return "Yes" + textToAppend;
            }

            return "No" + textToAppend;
        }

        public static string BoolToText(this bool source, string textIfTrue, string textIfFalse)
        {
            if (source)
            {
                return textIfTrue;
            }

            return textIfFalse;
        }

        /// <summary>
        /// Nullables the bool to yes no O empty.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string NullableBoolToYesNoOEmpty(this bool? source)
        {
            if (source == null)
            {
                return string.Empty;
            }

            if (source.Value)
            {
                return "Yes";
            }

            return "No";
        }

        /// <summary>
        /// Determines whether [contains] [the specified source].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="toCheck">To check.</param>
        /// <param name="comp">The comp.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
