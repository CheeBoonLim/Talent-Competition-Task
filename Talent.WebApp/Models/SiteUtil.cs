using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Talent.WebApp.Models
{
    public static class SiteUtil
    {
        public static bool IsDevMode
        {
            get
            {

                try
                {
                    return bool.Parse(WebConfigurationManager.AppSettings["IsDevMode"]);
                }
                catch
                {
                    return false;
                }
            }
        }

        public static string Version
        {
            get
            {

                try
                {
                    return WebConfigurationManager.AppSettings["CatReleaseVersion"].ToString();
                }
                catch
                {
                    return "1.0";
                }
            }
        }

        public static bool EnableOrderConfirmationModal
        {
            get
            {

                try
                {
                    return bool.Parse(WebConfigurationManager.AppSettings["EnableOrderConfirmationModal"].ToString());
                }
                catch
                {
                    return false;
                }
            }
        }

        public static int OrderValidForMins
        {
            get
            {

                try
                {
                    return int.Parse(WebConfigurationManager.AppSettings["OrderValidForMins"].ToString());
                }
                catch
                {
                    return 15;
                }
            }
        }

        public static string BankAccount
        {
            get
            {

                try
                {
                    return "00-0000-00000000-00";
                }
                catch
                {
                    return "00-0000-00000000-00";
                }
            }
        }

        public static List<string> AllowedFileExtensions
        {
            get
            {
                try
                {
                    var fileExtensions = WebConfigurationManager.AppSettings["AllowedFileExtensions"].ToString();
                    var list = fileExtensions.Split(',').ToList();

                    return list.ConvertAll(item => item.ToLowerInvariant());
                }
                catch
                {
                    return new List<string> { ".pdf", ".png", ".jpeg", ".jpg", ".gif" };
                }
            }
        }

        public static string RecaptchaSiteKey
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["RecaptchaSiteKey"].ToString();
                }
                catch
                {
                    return "6LdnxEwUAAAAAEsBlheT_y1Zd_Dc_AH77Lb0WQ7C";
                }
            }
        }

        public static string RecaptchaSecretKey
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["RecaptchaSecretKey"].ToString();
                }
                catch
                {
                    return "6LcENDEUAAAAALX5zKWgIHiqYJgAjUi1DwmYJd1X";
                }
            }
        }

        public static string UploadFileDir
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["UploadFileDir"].ToString();
                }
                catch
                {
                    return "~/Uploadfiles";
                }
            }
        }

        public static string EmailVerificationURL
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["EmailVerificationURL"].ToString();
                }
                catch
                {
                    return "http://uat.skin.example/Verification/VerifyEmail";
                }
            }
        }

        public static string GmailAddress
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["GmailAddress"].ToString();
                }
                catch
                {
                    return "lygx2017@gmail.com";
                }
            }
        }

        public static string SenderName
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["SenderName"].ToString();
                }
                catch
                {
                    return "SKIN";
                }
            }
        }

        public static string SupportEmail
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["SupportEmail"].ToString();
                }
                catch
                {
                    return "support@skin.example";
                }
            }
        }

        public static string WebsiteURL
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["WebsiteURL"].ToString();
                }
                catch
                {
                    return "http://uat.skin.example";
                }
            }
        }

        public static string UploadDocumentURL
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["UploadDocumentURL"].ToString();
                }
                catch
                {
                    return "http://uat.skin.example/UploadFiles/";
                }
            }
        }

        public static string GetIpAddress
        {
            get
            {
                try
                {
                    var ip = HttpContext.Current.Request.UserHostAddress;

                    if (ip == "::1")
                        ip = "127.0.0.1";

                    return ip;
                }
                catch
                {
                    return "127.0.0.1";
                }
            }
        }

        public static string LoginURL
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["LoginURL"].ToString();
                }
                catch
                {
                    return "http://uat.skin.example/Account/LogIn";
                }
            }
        }

        public static string ResetPasswordURL
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["ResetPasswordURL"].ToString();
                }
                catch
                {
                    return "http://uat.skin.example/Account/ResetPassword";
                }
            }
        }
    }
}