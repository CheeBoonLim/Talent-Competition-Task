using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Talent.Core
{
    public static class Util
    {
        //const string SMSGLOBAL_SEND_SMS_URL = "https://www.smsglobal.co.nz/http-api.php?action=sendsms&user={0}&password={1}&from={2}&to={3}&text={4}&userfield={5}&api={6}&maxsplit={7}";
        const string SMSGLOBAL_SEND_SMS_URL = "https://www.smsglobal.com/http-api.php?action=sendsms&user={0}&password={1}&from={2}&to={3}&text={4}&userfield={5}&api={6}&maxsplit={7}";
        const string CLICKATELL_SEND_SMS_URL = "http://api.clickatell.com/http/sendmsg?user={0}&password={1}&api_id=3584992&to={2}&text={3}";

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static string SendSMS(string phoneNumber, string message, int id, bool is2way, Action<string> onSending = null)
        {
            string username = "hv6m1mkc"; //TODO: ConfigurationManager.AppSettings["SmsGlobalUsername"];
            string password = "JDJMwrKS"; // ConfigurationManager.AppSettings["SmsGlobalPassword"];
            string from = ""; // ConfigurationManager.AppSettings["SmsGlobalFrom"];

            double phoneDigit;
            if (!phoneNumber.IsNullOrEmpty() && !phoneNumber.StartsWith("64") && !double.TryParse(phoneNumber, out phoneDigit))
            {
                return "Invalid phone number.";
            }

            message = HttpUtility.UrlEncode(HttpUtility.UrlEncode(HttpUtility.UrlEncode(message))); // message has to be triple-encoded - as recommended by SMSGlobal
            var requestUrl = string.Format(SMSGLOBAL_SEND_SMS_URL, username, password, from, phoneNumber, message, id, is2way ? 1 : 0, 10);

            if (onSending != null)
            {
                onSending(requestUrl);
            }

            using (var client = new WebClient())
            {
                var response = client.DownloadString(requestUrl);
                return response;
            }
        }

        /// <summary>
        /// Get Random Six Digit Number
        /// </summary>
        /// <returns></returns>
        public static string GetRandomSixDigitNumber()
        {
            Random r = new Random();
            int randNum = r.Next(1000000);
            return randNum.ToString("D6");
        }

        /// <summary>
        /// Get Phone Number
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string GetPhoneNumber(string dialCode, string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                // Remove this first number is 0
                if (phoneNumber[0].Equals('0'))
                    phoneNumber = phoneNumber.Substring(1);
            }
            return string.Format("+{0}{1}", dialCode, phoneNumber);
        }
    }
}
