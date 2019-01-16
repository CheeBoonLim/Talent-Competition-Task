using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Talent.Core
{
    /// <summary>
    /// Represents object util
    /// </summary>
    /// 13/11/2009
    public static class ObjectUtil
    {
        /// <summary>
        /// Throws if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src">The source object.</param>
        /// 13/11/2009
        public static void ThrowIfNull<T>(this T src) where T : class
        {
            if (src == null)
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Throws if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src">The source object.</param>
        /// <param name="paramName">Name of the param.</param>
        /// 13/11/2009
        public static void ThrowIfNull<T>(this T src, string paramName) where T : class
        {
            if (src == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// To the property dictionary.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The object of <see cref="Dictionary&lt;System.String,System.Object&gt;"/>
        /// </returns>
        /// 16/11/2009
        public static Dictionary<string, object> ToPropertyDictionary(this object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "source can not be null.");
            }

            var result = new Dictionary<string, object>();
            var properties = TypeDescriptor.GetProperties(source);

            foreach (PropertyDescriptor property in properties)
            {
                result.Add(property.Name, property.GetValue(source));
            }

            return result;
        }


        /// <summary>
        /// Gets the exception text.
        /// </summary>
        /// <param name="x">The ex.</param>
        /// <returns></returns>
        public static string GetExceptionAndInnerText(this Exception x)
        {
            if (x == null)
            {
                return string.Empty;
            }
            else if (x.InnerException == null)
            {
                return x.ToString();
            }
            else
            {
                return x.ToString() + "\n" + GetExceptionAndInnerText(x.InnerException);
            }
        }

        public static string GetExceptionAnd1LevelInnerText(this Exception x)
        {
            if (x == null)
            {
                return string.Empty;
            }
            else if (x.InnerException == null)
            {
                return x.ToString();
            }
            else
            {
                return x.ToString() + "\n" + x.InnerException.ToString();
            }
        }
    }
}
