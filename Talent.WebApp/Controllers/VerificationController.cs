using Talent.Data;
using Talent.Data.Entities;
using Talent.Diagnostics;
using Talent.Service.Persons;
using Talent.Service.SendMail;
using Talent.WebApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Talent.WebApp.Controllers
{
    public class VerificationController : Controller
    {
        private IRepository<Login> _loginRepository;
        private IRepository<Person> _personRepository;
        private IPersonService _personService;
        private IEmailService _emailService;

        public VerificationController(IRepository<Login> loginRepository,
                                        IRepository<Person> personRepository,
                                        IPersonService personService,
                                        IEmailService emailService)
        {
            _loginRepository = loginRepository;
            _personRepository = personRepository;
            _personService = personService;
            _emailService = emailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Verify Email
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyEmail(string email, Guid emailCode)
        {
            try
            {
                // verify email
                var user = _loginRepository.GetQueryable().FirstOrDefault(x => x.EmailCode == emailCode);

                if (user == null || string.IsNullOrEmpty(user.Username) || !user.Username.Equals(email))
                    return RedirectToAction("VerifyEmailError", "Verification");

                // If expire, resend email code
                if (user.ExpiredOn.GetValueOrDefault() < DateTime.UtcNow && !user.EmailAddressAuthorized)
                    return RedirectToAction("VerifyEmailExpire", "Verification", new { email = user.Username });

                // update EmailAddressAuthorized
                user.EmailAddressAuthorized = true;
                user.IsDisabled = false;

                _loginRepository.Update(user);

                if (user != null && user.Person != null)
                {
                    // Send a verification email notification to user.
                    _emailService.SendVerifiedEmailNotification(SiteUtil.LoginURL, SiteUtil.SupportEmail,
                                                                user.Username, SiteUtil.SenderName, user);

                    // Send a verification email notification to support.
                    var senderName = string.Format("{0} {1}", user.Person.FirstName, user.Person.LastName);
                    _emailService.SendVerifiedEmailNotification(SiteUtil.LoginURL, user.Username,
                                                                SiteUtil.SupportEmail, senderName, user);
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
                ModelState.AddModelError("", "Something went wrong, our team has been notified with this error.");
                return RedirectToAction("VerifyEmailError", "Verification");
            }

            return RedirectToAction("VerifyEmailSuccess", "Verification");
        }

        public ActionResult VerifyEmailError()
        {
            return View();
        }

        public ActionResult VerifyEmailSuccess()
        {
            return View();
        }

        public ActionResult VerifyEmailExpire(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult VerifyMobileCode(int id, string smsCode)
        {
            try
            {
                // Get user by mobile code
                var user = _personRepository.GetQueryable()
                                            .FirstOrDefault(x => x.MobileCode == smsCode
                                                            && x.Id == id
                                                            && x.IsVerifyMobile != true);
                // Check correct user
                if (user == null)
                    return Json(new { success = false, message = "The SMS Code is invalid." });

                //If expire in 60 seconds, Ask to resend sms code
                if (user.MobileCodeGenerated.GetValueOrDefault() < DateTime.UtcNow.AddSeconds(-60))
                    return Json(new { success = false, message = "The SMS Code is expired. Please click the Send SMS Code button to resend." });

                // update IsVerifyMobile
                user.IsVerifyMobile = true;
                _personRepository.Update(user);

            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
                return Json(new { success = false, message = "Something went wrong, Please contact to the support team" });
            }

            return Json(new { success = true, message = "Your mobile number has been verified" });
        }
    }
}