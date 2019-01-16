using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Threading;
using System.Security.Cryptography;
using System;

namespace Talent.Core
{
    /// <summary>
    /// Represents SecurityUtil.
    /// </summary>
    public static class SecurityUtil
    {
        /// <summary>
        /// Trusts all certificates.
        /// </summary>
        /// <remarks>
        /// By calling this method, the current application will trust
        /// all remote certificates disregarding if they contain any error.
        /// Note, applying strict SSL certificate validation is a better practice.
        /// </remarks>
        public static void TrustAllCertificates()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
                {
                    if (sslPolicyErrors != SslPolicyErrors.None)
                    {
                        Debug.WriteLine(string.Format("Trusting an invalid certificate {0}: {1}",
                                                        certificate != null ? certificate.Subject : "Unknown",
                                                        sslPolicyErrors));
                    }

                    return true;
                };
        }

        const string DEFAULT_USERNAME = "default";


        /// <summary>
        /// Gets the salt.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GetSalt(int length)
        {
            //Create and populate random byte array
            byte[] randomArray = new byte[length];

            string randomString;

            //Create random salt and convert to string
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);
            randomString = Convert.ToBase64String(randomArray);
            return randomString;
        }
    }
}
