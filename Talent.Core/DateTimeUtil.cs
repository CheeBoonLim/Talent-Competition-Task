using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Talent.Core
{
    /// <summary>
    /// Represents DateTime Utilities
    /// </summary>
    public static class DateTimeUtil
    {
        private const string STR_NewZealandStandardTime = "New Zealand Standard Time";

        /// <summary>
        /// Gets the nights to the specified end date.
        /// </summary>
        /// <remarks>
        /// This method calculates the number of nights in general rules.
        /// Different parties may have their own way of interpreting nights.
        /// e.g. 2009-May-01 01:00:00 to 2009-May-03:23:00:00
        /// will return you 2 nights by this method.
        /// </remarks>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The number of nights</returns>
        public static double GetNightsTo(this DateTime startDate, DateTime endDate)
        {
            if (!startDate.Kind.Equals(endDate.Kind))
            {
                throw new ArgumentException("End date should have the same kind as start date.e.g. Utc,Local", "endDate");
            }

            if (endDate < startDate)
            {
                throw new ArgumentException("End date should not be less than start date.", "endDate");
            }

            return (new DateTime(endDate.Year, endDate.Month, endDate.Day)
                    - new DateTime(startDate.Year, startDate.Month, startDate.Day)).TotalDays;
        }

        public static int MonthDifference(this DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }

        /// <summary>
        /// Friendlies the date.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <returns></returns>
        public static string ToFriendlyDate(this DateTime src)
        {
            var value = src;

            var now = DateTime.Now;
            var yesterday = DateTime.Now.AddDays(-1);

            if (value.Date.Equals(now.Date))
            {
                return value.ToString("hh:mmtt") + " today";
            }
            else if (value.Date.Equals(yesterday.Date))
            {
                return value.ToString("hh:mmtt") + " yesterday";
            }
            else
            {
                return value.ToString("hh:mmtt dd/MMM");
            }
        }

        /// <summary>
        /// Periods the of day.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <returns></returns>
        public static string PeriodOfDay(this DateTime src)
        {
            var h = src.Hour;

            if (h >= 5 && h < 12)
            {
                return "morning";
            }
            else if (h >= 12 && h < 13)
            {
                return "noon";
            }
            else if (h >= 13 && h < 18)
            {
                return "afternoon";
            }
            else if (h >= 18 && h < 21)
            {
                return "evening";
            }
            else if (h >= 21 && h < 24)
            {
                return "night";
            }
            else if (h == 0)
            {
                return "midnight";
            }
            else
            {
                return "late night";
            }
        }

        public static string HowLongAgoRoughly(this DateTime utcDate)
        {
            var utcNow = DateTime.UtcNow;

            TimeSpan diff = utcNow - utcDate;

            if (diff.TotalHours > 24)
            {
                if (diff.TotalDays > 31)
                {
                    return string.Format("{0:0.0} months ago", diff.TotalDays / 31);
                }

                return string.Format("{0:0.0} days ago", diff.TotalDays);
            }
            if (diff.TotalMinutes > 60)
            {
                return string.Format("{0:0.0} hrs ago", diff.TotalHours);
            }
            if (diff.TotalSeconds > 60)
            {
                return string.Format("{0:0} mins ago", diff.TotalMinutes);
            }

            return string.Format("{0:0} secs ago", diff.TotalSeconds < 0 ? 0 : diff.TotalSeconds);
        }

        /// <summary>
        /// How long ago roughly.
        /// </summary>
        /// <param name="utcDate">The UTC date.</param>
        /// <returns>A <see cref="string"/> value that represents how long ago.</returns>
        public static string HowLongAgoRoughly(this DateTime utcDate, string timeZone)
        {
            if (utcDate.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("The date must be Utc date.", "utcDate");
            }

            if (utcDate.Equals(new DateTime()))
            {
                return "N/A";
            }

            // converts back to localtime
            var temp = utcDate.ToLocalTime();
            var tempUtc = temp.ToUniversalTimeWithTimeZone(timeZone);

            var utcNow = DateTime.UtcNow;

            TimeSpan diff = utcNow - tempUtc;

            if (diff.TotalHours > 24)
            {
                if (diff.TotalDays > 31)
                {
                    return string.Format("{0:0.0} months ago", diff.TotalDays / 31);
                }

                return string.Format("{0:0.0} days ago", diff.TotalDays);
            }
            if (diff.TotalMinutes > 60)
            {
                return string.Format("{0:0.0} hrs ago", diff.TotalHours);
            }
            if (diff.TotalSeconds > 60)
            {
                return string.Format("{0:0} mins ago", diff.TotalMinutes);
            }

            return string.Format("{0:0} secs ago", diff.TotalSeconds < 0 ? 0 : diff.TotalSeconds);
        }

        /// <summary>
        /// Hows the long roughly to.
        /// </summary>
        /// <param name="utcDate">The UTC date.</param>
        /// <param name="endUtcDate">The end UTC date.</param>
        /// <returns></returns>
        public static string HowLongRoughlyTo(this DateTime utcDate, DateTime endUtcDate)
        {
            if (utcDate.Kind != DateTimeKind.Utc)
                throw new ArgumentException("The date must be Utc date.", "utcDate");

            if (endUtcDate.Kind != DateTimeKind.Utc)
                throw new ArgumentException("The date must be Utc date.", "endUtcDate");


            TimeSpan diff = endUtcDate - utcDate;

            if (diff.TotalHours > 24)
            {
                if (diff.TotalDays > 31)
                {
                    return string.Format("{0:0.0}months", diff.TotalDays / 31);
                }

                return string.Format("{0:0.0}days", diff.TotalDays);
            }
            if (diff.TotalMinutes > 60)
            {
                return string.Format("{0:0.0}hrs", diff.TotalHours);
            }
            if (diff.TotalSeconds > 60)
            {
                return string.Format("{0:0}mins", diff.TotalMinutes);
            }

            return string.Format("{0:0}secs", diff.TotalSeconds < 0 ? 0 : diff.TotalSeconds);
        }

        public static DateTime FromUtc(this DateTime src, string targetTimezone)
        {
            TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById(targetTimezone);
            var dateTime = TimeZoneInfo.ConvertTimeFromUtc(src, timezone);
            return dateTime;
        }

        /// <summary>
        /// Converts local time to NZ time.
        /// </summary>
        /// <remarks>
        /// if the dateTime is of DateTimeKind.Unspecified, it will be treated as same as DateTimeKind.Local and then converts to 
        /// New Zealand time
        /// </remarks>
        /// <param name="dateTime">The date time.</param>
        /// <param name="specifyLocal">if set to <c>true</c> specify the converted datetime to be DateTimeKind.Local; Otherwise, the converted dateTime will be DateTimeKind.Unspecified.</param>
        /// <returns>The object of <see cref="DateTime"/>
        /// </returns>
        /// 15/10/2009
        public static DateTime ConvertLocalToNZTime(this DateTime localDateTime, bool specifyLocal)
        {
            DateTime nzLocal;
            TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById(STR_NewZealandStandardTime);

            if (localDateTime.Kind == DateTimeKind.Unspecified)
            {
                localDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime, TimeZoneInfo.Local);
                nzLocal = TimeZoneInfo.ConvertTimeFromUtc(localDateTime, timezone);
            }
            else if (localDateTime.Kind == DateTimeKind.Local)
            {
                nzLocal = TimeZoneInfo.ConvertTime(localDateTime, timezone);
            }
            else
            {
                nzLocal = TimeZoneInfo.ConvertTimeFromUtc(localDateTime, timezone);
            }

            nzLocal = specifyLocal ? DateTime.SpecifyKind(nzLocal, DateTimeKind.Local)
                    : DateTime.SpecifyKind(nzLocal, DateTimeKind.Unspecified);

            return nzLocal;
        }



        /// <summary>
        /// Converts the NZ time to local time.
        /// </summary>
        /// <remarks>
        /// if the dateTime is of DateTimeKind.Unspecified, it will be treated as same as NZ datetime with DateTimeKind.Local and then converts to the server's local time.
        /// </remarks>
        /// <param name="nzDateTime">The nz date time.</param>
        /// <param name="specifyLocal">if set to <c>true</c> specify the converted datetime to be DateTimeKind.Local; Otherwise, the converted dateTime will be DateTimeKind.Unspecified.</param>
        /// <returns>The object of <see cref="DateTime"/>
        /// </returns>
        /// 16/10/2009
        public static DateTime ConvertNZToLocalTime(this DateTime nzDateTime, bool specifyLocal)
        {
            DateTime local;
            TimeZoneInfo timezone = TimeZoneInfo.Local;
            TimeZoneInfo nztimezone = TimeZoneInfo.FindSystemTimeZoneById(STR_NewZealandStandardTime);

            if (nzDateTime.Kind == DateTimeKind.Unspecified)
            {
                nzDateTime = TimeZoneInfo.ConvertTimeToUtc(nzDateTime, nztimezone);
                local = TimeZoneInfo.ConvertTimeFromUtc(nzDateTime, timezone);
            }
            else if (nzDateTime.Kind == DateTimeKind.Local)
            {
                local = TimeZoneInfo.ConvertTime(nzDateTime, timezone);
            }
            else
            {
                local = TimeZoneInfo.ConvertTimeFromUtc(nzDateTime, timezone);
            }

            local = specifyLocal ? DateTime.SpecifyKind(local, DateTimeKind.Local)
                    : DateTime.SpecifyKind(local, DateTimeKind.Unspecified);

            return local;
        }

        /// <summary>
        /// Converts the universal time with time zone.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="srcTimeZone">The SRC time zone.</param>
        /// <returns></returns>
        public static DateTime ToUniversalTimeWithTimeZone(this DateTime src, string srcTimeZone)
        {
            src = DateTime.SpecifyKind(src, DateTimeKind.Unspecified);
            TimeZoneInfo tzInfo = TimeZoneInfo.FindSystemTimeZoneById(srcTimeZone);
            var converted = TimeZoneInfo.ConvertTimeToUtc(src, tzInfo);
            return converted;
        }

        /// <summary>
        /// Determines whether [is same day] [the specified SRC].
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="date">The date.</param>
        /// <returns>
        ///   <c>true</c> if [is same day] [the specified SRC]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSameDayAs(this DateTime src, DateTime date)
        {
            return src.Year == date.Year && src.Month == date.Month && src.Day == date.Day;
        }


        /// <summary>
        /// Converts from unix timestamp.
        /// </summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns></returns>
        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        /// <summary>
        /// To the unix timespan.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static long ToUnixTimespan(DateTime date, bool specifyLocal = false)
        {
            if (specifyLocal)
            {
                DateTime.SpecifyKind(date, DateTimeKind.Local);
            }

            TimeSpan tspan = date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)Math.Truncate(tspan.TotalSeconds);
        }
    }
}
