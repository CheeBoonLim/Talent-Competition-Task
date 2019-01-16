using Talent.Service.Domain;
using Talent.WebApp.Models;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Web.Mvc;

namespace Talent.WebApp.Controllers
{
    public class GeneralBaseController : Controller
    {
        protected IApplicationContext _appContext;

        public GeneralBaseController(IApplicationContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Validate Google recaptcha 
        /// </summary>
        /// <returns></returns>
        public bool IsValidateReCaptcha()
        {
            var response = Request["g-recaptcha-response"];
            //string secretKey = "6LcENDEUAAAAALX5zKWgIHiqYJgAjUi1DwmYJd1X";
            string secretKey = SiteUtil.RecaptchaSecretKey;
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            return status;
        }
    }
}