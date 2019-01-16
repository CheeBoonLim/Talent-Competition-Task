using Talent.Data;
using Talent.Data.Entities;
using Talent.Diagnostics;
using Talent.Service.Domain;
using Talent.Core;
using Talent.Service.Models;
using Talent.Service.Persons;
using Talent.Service.SendMail;
using Talent.Service.SignUp;
using Talent.WebApp.Models;
using Talent.WebApp.Models.Account;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Talent.WebApp.Models.Admin;

namespace Talent.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private ISignUpService _signUpService;
        private IEmailService _emailService;
        private IApplicationContext _appContext;
        private IRepository<Login> _loginRepository;
        private IRepository<PersonDocument> _personDocumentRepository;
        private IPersonService _personService;
        private IRepository<Person> _personRepository;

        private Regex DigitsRegex = new Regex("[0-9]", RegexOptions.Compiled);
        private Regex UpperCaseRegex = new Regex("[A-Z]", RegexOptions.Compiled);
        private Regex LowerCaseRegex = new Regex("[a-z]", RegexOptions.Compiled);
        private Regex SpecialCharsRegex = new Regex("[!@#$%^&_+.,;:]", RegexOptions.Compiled);

        const string SEND_SMS_MESSAGE = "Welcome to SKIN, Your verification code is {0}." +
                                        " Use this to complete your verification mobile number. Thank you";

        public AccountController(ISignUpService signUpService,
                                IEmailService emailService,
                                IApplicationContext applicationContext,
                                IRepository<Login> loginRepository,
                                IRepository<PersonDocument> personDocumentRepository,
                                IPersonService personService,
                                IRepository<Person> personRepository)
        {
            _signUpService = signUpService;
            _emailService = emailService;
            _appContext = applicationContext;
            _loginRepository = loginRepository;
            _personDocumentRepository = personDocumentRepository;
            _personService = personService;
            _personRepository = personRepository;
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            var loginId = _appContext.LoginId;
            var userProfile = GetUserProfileModel(loginId);
            ViewBag.IsAdmin = false;
            ViewBag.CurrentPage = Pages.MyProfile;

            return View(userProfile);
        }

        //[Authorize]
        public ActionResult Profile()
        {
            return View("Dashboard");
        }

        //public ActionResult Dashboard()
        //{
        //    var view = PartialView();
        //    return Json(new { success = true, data = view }, JsonRequestBehavior.AllowGet);
        //}

        [Authorize]
        public ActionResult Verification()
        {
            var loginId = _appContext.LoginId;
            var userProfile = GetUserProfileModel(loginId);

            ViewBag.FileExtentions = SiteUtil.AllowedFileExtensions;
            ViewBag.IsAdmin = false;
            ViewBag.CurrentPage = Pages.Verification;

            return View(userProfile);
        }

        // Note this is ADMIN only
        [Authorize(Roles = "Admin")]
        public ActionResult UserProfile(int id)
        {
            var userProfile = GetUserProfileModel(id);

            ViewBag.FileExtentions = SiteUtil.AllowedFileExtensions;
            ViewBag.IsAdmin = true;

            ViewBag.CurrentPage = Pages.MyProfile;

            return View("MyProfile", userProfile);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult GetDocumentProfile(int id)
        {
            var documents = GetUserProfileModel(id);
            ViewBag.IsAdmin = true;
            return PartialView("_DocumentProfile", documents);
        }

        /// <summary>
        /// Load register form
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            var registerModel = new RegisterModel();
            registerModel.RecaptchaSiteKey = SiteUtil.RecaptchaSiteKey;
            return View(registerModel);
        }

        /// <summary>
        /// Create new customer account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            try
            {
                model.RecaptchaSiteKey = SiteUtil.RecaptchaSiteKey;

                //if (!IsValidateReCaptcha())
                //{
                //    ModelState.AddModelError("", "Google reCaptcha validation failed.");
                //    return View(model);
                //}

                if (ModelState.IsValid)
                {
                    // Check password format
                    //if (!ValidatePassword(model.Password))
                    //{
                    //    ModelState.AddModelError("Password", "The password is not correct format");

                    //    return Json(new { Success = false, Message = "The password is not correct format" });
                    //}

                    if (_loginRepository.GetQueryable().Any(n => n.Username == model.Username))
                    {
                        //ModelState.AddModelError("", "Email has been used, " +
                        //                        "please click forget password to reset your password");
                        return Json(new { Success = false, Message = "Email has been used, please click forget password to reset your password" });
                    }
                    //Disable mobile number for now
                    var user = new SignUpPersonal()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MobileNumber = "",
                        EmailAddress = model.Username,
                        Password = model.Password,
                        CountryCode = "",
                        DialCode = "",
                        EmailCode = Guid.NewGuid(),
                        TermsConditionsAccepted = true
                    };

                    // Save to database
                    _signUpService.Register(user);

                    // Send a verification email to user
                    _emailService.SendMail(SiteUtil.WebsiteURL, SiteUtil.GmailAddress, model.Username,
                                            user.EmailCode, SiteUtil.EmailVerificationURL, SiteUtil.SenderName);

                    // Send a new register notification to support
                    _emailService.SendNewRegisterNotification(SiteUtil.SupportEmail, user);

                    return Json(new { Success = true });
                }
                else
                {
                    //ModelState.AddModelError("", "Data is not correct");
                    return Json(new { Success = false, Message = "Data is not correct" });
                }
            }
            catch (Exception x)
            {
                Logging.Error(x.Message, x);
                ModelState.AddModelError("", "Something went wrong, " +
                                            "our team has been notified with this error.");
            }
            return Json(new { Success = false, Message = "Something went wrong, " +
                                            "our team has been notified with this error."
            });
        }

        /// <summary>
        /// Load log in form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn(string returnURL)
        {

            var userinfo = new LoginModel();
            userinfo.RecaptchaSiteKey = SiteUtil.RecaptchaSiteKey;

            try
            {
                // We do not want to use any existing identity information
                EnsureLoggedOut();

                // Store the originating URL so we can attach it to a form field
                userinfo.ReturnURL = returnURL;

                return View(userinfo);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Check and login account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginModel user)
        {
            //user.RecaptchaSiteKey = SiteUtil.RecaptchaSiteKey;

            //if (!IsValidateReCaptcha())
            //{
            //    ModelState.AddModelError("", "Google reCaptcha validation failed.");
            //    return View(user);
            //}

            if (ModelState.IsValid)
            {
                if (_signUpService.VerifyPassword(user.Username, user.Password))
                {
                    var login = _loginRepository.GetQueryable()
                                .Where(x => x.Username == user.Username).FirstOrDefault();

                    if (login != null && login.IsDisabled)
                    {
                        ModelState.AddModelError("", "Your account is disable. " +
                                                "Please contact to support team!");
                        return View(user);
                    }

                    //Login Success
                    //For Set Authentication in Cookie (Remeber ME Option)
                    SignInRemember(user, login);
                    _appContext.SignIn(login.Username);
                    // Send a login notification to user
                    if (login != null && login.Person != null)
                    {
                        var userEmail = login.Username;
                        var fullName = string.Format("{0} {1}",
                                        login.Person.FirstName, login.Person.LastName);
                    }
                    return Json(new { Success = true });
                }
                else
                {
                    ModelState.AddModelError("", "Username and password does not match.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }
            return View(user);
        }


        //GET: SignInAsync   
        private void SignInRemember(LoginModel user, Login login)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();


            var roles = new List<string>();

            roles.Add("Customer"); // default

            if (login.IsAdmin)
            {
                roles.Add("Admin");
            }
            var rolesText = string.Join(",", roles.ToArray());

            var formAuthTicket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now,
                DateTime.Now.AddMinutes(30), user.IsRemember, rolesText, FormsAuthentication.FormsCookiePath);

            // Write the authentication cookie

            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,
                                                FormsAuthentication.Encrypt(formAuthTicket)));
        }

        /// <summary>
        /// Log off account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Account");
        }

        /// <summary>
        /// Resend email code
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        // TODO: Must check HttpPost why it does not work
        public ActionResult ResendEmailCode(string email)
        {
            try
            {
                var user = _loginRepository.GetQueryable()
                            .FirstOrDefault(x => x.Username == email && !x.EmailAddressAuthorized);

                if (user == null)
                {
                    ModelState.AddModelError("", "This email is invalid");
                }
                else
                {
                    var newEmailCode = Guid.NewGuid();
                    user.EmailCode = newEmailCode;
                    user.ExpiredOn = DateTime.UtcNow.AddHours(24);

                    // Update new email code and expired date
                    _loginRepository.Update(user);

                    // Send verification email
                    _emailService.SendMail(SiteUtil.WebsiteURL, SiteUtil.GmailAddress, user.Username,
                                            newEmailCode, SiteUtil.EmailVerificationURL, SiteUtil.SenderName);

                    return RedirectToAction("Index", "Verification");
                }
            }
            catch (Exception x)
            {
                Logging.Error(x.Message, x);
                ModelState.AddModelError("", "Something went wrong, " +
                    "our team has been notified with this error.");
            }

            return RedirectToAction("VerifyEmailError", "Verification");
        }

        [HttpPost]
        [Authorize]
        public ActionResult SendSMS(int id, string phoneNumber)
        {
            try
            {
                var person = _personRepository.GetById(id);

                if (person == null)
                    return Json(new { success = false, message = "Your id is invalid." });

                // Send SMS code
                var smsNumber = Util.GetPhoneNumber(person.DialCode, phoneNumber);
                var smsCode = Util.GetRandomSixDigitNumber();
                var smsMessage = string.Format(SEND_SMS_MESSAGE, smsCode);

                var isSent = Util.SendSMS(smsNumber, smsMessage, id, true);

                // TODO: check send ok or not?

                // Save SMS code to database
                _personService.UpdateMobileCode(id, smsCode);
            }
            catch (Exception x)
            {
                Logging.Error(x.Message, x);
                return Json(new
                {
                    success = false,
                    message = "Something went wrong, " +
                    "our team has been notified with this error."
                });
            }

            return Json(new
            {
                success = true,
                message = "The SMS Code has been sent to your mobile number."
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult UploadFile(HttpPostedFileBase fileName, int documentType)
        {
            var loginId = _appContext.LoginId;
            var login = _loginRepository.GetQueryable().Where(x => x.Id == loginId).FirstOrDefault();

            if (login == null || login.Person == null)
            {
                ModelState.AddModelError("", "This login is invalid");
                return RedirectToAction("MyProfile");
            }

            if (fileName == null)
            {
                ModelState.AddModelError("", "Please select a file to upload");
                return RedirectToAction("MyProfile");
            }

            try
            {
                // TODO put it in the service PersonDocumentService
                var allowedExtensions = SiteUtil.AllowedFileExtensions;
                var extension = Path.GetExtension(fileName.FileName);
                var guid = login.Person.UID;

                if (allowedExtensions.Any(e => e.Equals(extension, StringComparison.InvariantCultureIgnoreCase)))
                {
                    //var newName = "new-name" + extension;
                    var newFileName = GetNewFileName(documentType, loginId, guid) + extension;
                    //string path = Path.Combine(Server.MapPath("~/UploadFiles"), newFileName);
                    string path = Path.Combine(Server.MapPath(SiteUtil.UploadFileDir), newFileName);
                    fileName.SaveAs(path);

                    //save data
                    var personDocument = new PersonDocument
                    {
                        FileName = newFileName,
                        DocumentType = documentType,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = loginId,
                        PersonId = loginId,
                        Status = (int)DocumentStatus.Processing
                    };
                    _personDocumentRepository.Add(personDocument);

                    // Send notification
                    var docType = Enum.GetName(typeof(DocumentType), documentType);
                    var fullName = string.Format("{0} {1}", login.Person.FirstName, login.Person.LastName);

                    _emailService.SendUploadDocumentNotification(SiteUtil.UploadDocumentURL, login.Username, SiteUtil.SupportEmail,
                                                                    newFileName, docType, fullName, login.Person.MobilePhone);
                }
                else
                {
                    ModelState.AddModelError("", "Please select a " + string.Join(" ", allowedExtensions.ToArray()) + " file");
                }

            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message, ex);
                ModelState.AddModelError("", "Something went wrong, " +
                                        "our team has been notified with this error.");
            }

            return RedirectToAction("MyProfile");
        }

        #region Forgot password

        public ActionResult ForgotPassword()
        {
            var model = new ForgotPasswordModel();
            model.RecaptchaSiteKey = SiteUtil.RecaptchaSiteKey;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordModel user)
        {
            user.RecaptchaSiteKey = SiteUtil.RecaptchaSiteKey;

            if (!IsValidateReCaptcha())
            {
                ModelState.AddModelError("", "Google reCaptcha validation failed.");
                return View(user);
            }

            if (ModelState.IsValid)
            {

                var login = _loginRepository.GetQueryable()
                            .Where(x => x.Username == user.Email).FirstOrDefault();

                if (login == null)
                {
                    ModelState.AddModelError("", "Your email is invalid. " +
                                                "Please contact to support team!");
                    return View(user);
                }

                if (!login.EmailAddressAuthorized)
                {
                    ModelState.AddModelError("", "Your email has not been verified. " +
                        "Please check your inbox email or contact to support team!");
                    return View(user);
                }

                if (login.IsDisabled)
                {
                    ModelState.AddModelError("", "Your account is disable. " +
                        "Please contact to support team!");
                    return View(user);
                }

                // Send a forgot password email to user
                var uid = Server.UrlEncode(login.PasswordHash);
                var resetPasswordUrl = string.Format("{0}?email={1}&uid={2}",
                                        SiteUtil.ResetPasswordURL, user.Email, uid);
                // Send an email
                _emailService.SendForgotPassword(resetPasswordUrl, SiteUtil.SupportEmail,
                                                    user.Email, SiteUtil.SenderName);

                return RedirectToAction("SentForgotPassword");

            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. Please contact to support team!");
            }
            return View(user);
        }

        public ActionResult SentForgotPassword()
        {
            return View();
        }

        public ActionResult ResetPassword(string email, string uid)
        {
            var model = new ResetPasswordModel();
            model.RecaptchaSiteKey = SiteUtil.RecaptchaSiteKey;
            model.Email = email;
            model.OldPassword = uid;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            model.RecaptchaSiteKey = SiteUtil.RecaptchaSiteKey;

            if (!IsValidateReCaptcha())
            {
                ModelState.AddModelError("", "Google reCaptcha validation failed.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                // Check password format
                if (!ValidatePassword(model.NewPassword))
                {
                    ModelState.AddModelError("Password", "The password is not correct format");
                    return View(model);
                }

                var login = _loginRepository.GetQueryable()
                            .Where(x => x.Username == model.Email).FirstOrDefault();

                if (login == null || login.PasswordHash != model.OldPassword)
                {
                    ModelState.AddModelError("", "Your account is invalid. " +
                                                "Please contact to support team!");
                    return View(model);
                }

                if (!login.EmailAddressAuthorized)
                {
                    ModelState.AddModelError("", "Your email has not been verified. " +
                        "Please check your inbox email or contact to support team!");
                    return View(model);
                }

                if (login.IsDisabled)
                {
                    ModelState.AddModelError("", "Your account is disable. " +
                        "Please contact to support team!");
                    return View(model);
                }

                // everything is ok. update a new password
                _signUpService.ResetPassword(login, model.NewPassword);

                return RedirectToAction("ResetPasswordSuccess");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. Please contact to support team!");
            }

            return View(model);
        }

        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }

        #endregion

        #region --> Pre Required Method For Login

        //GET: EnsureLoggedOut
        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }

        //POST: Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                FormsAuthentication.SignOut();

                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();

                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                return RedirectToLocal();
            }
            catch
            {
                throw;
            }
        }

        //GET: RedirectToLocal
        private ActionResult RedirectToLocal(string returnURL = "")
        {
            try
            {
                // If the return url starts with a slash "/" we assume it belongs to our site
                // so we will redirect to this "action"
                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                    return Redirect(returnURL);

                // If we cannot verify if the url is local to our host we redirect to a default location
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Helper
        /// <summary>
        /// Check password format
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        bool ValidatePassword(string password)
        {
            int lower = LowerCaseRegex.Matches(password).Count;
            int upper = UpperCaseRegex.Matches(password).Count;
            int special = SpecialCharsRegex.Matches(password).Count;
            int digit = DigitsRegex.Matches(password).Count;
            if (lower > 0 && upper > 0 && special > 0 && digit > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Validate Google recaptcha 
        /// </summary>
        /// <returns></returns>
        bool IsValidateReCaptcha()
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

        string GetNewFileName(int documentType, int id, Guid guid)
        {
            var countNumber = _personDocumentRepository.GetQueryable()
                        .Where(x => x.PersonId == id && x.DocumentType == documentType).Count();

            countNumber++;

            string fileName = string.Empty;
            switch (documentType)
            {
                case (int)DocumentType.BankStatement:
                    fileName = string.Format("BankStatement_{0}_{1}_{2}", id, guid, countNumber);
                    break;
                case (int)DocumentType.AddressProof:
                    fileName = string.Format("AddressProof_{0}_{1}_{2}", id, guid, countNumber);
                    break;
                case (int)DocumentType.IssuedID:
                    fileName = string.Format("IssuedID_{0}_{1}_{2}", id, guid, countNumber);
                    break;
                case (int)DocumentType.SignedSelfie:
                    fileName = string.Format("SignedSelfie_{0}_{1}_{2}", id, guid, countNumber);
                    break;
            }
            return fileName;
        }

        protected UserProfileModel GetUserProfileModel(int id)
        {
            var verification = _personService.GetVerification(id);

            var userProfile = _loginRepository.GetQueryable().Where(x => x.Id == id).AsEnumerable()
               .Select(x => new UserProfileModel
               {
                   Id = x.Id,
                   Username = x.Username,
                   UID = x.Person != null ? x.Person.UID : Guid.Empty,
                   FirstName = x.Person != null ? x.Person.FirstName : string.Empty,
                   MiddleName = x.Person != null ? x.Person.MiddleName : string.Empty,
                   LastName = x.Person != null ? x.Person.LastName : string.Empty,
                   Mobile = x.Person != null ? x.Person.MobilePhone : string.Empty,
                   DateOfBirth = (x.Person != null && x.Person.DateOfBirth != null) ? (x.Person.DateOfBirth.HasValue ? x.Person.DateOfBirth.Value.ToString("dd/MM/yyyy") : string.Empty) : string.Empty,
                   VerificationLevel = (int)verification.Level,
                   VerificationLevelName = verification.LevelName,
                   VerificationLevelNameCssColor = verification.Level == VerificationLevel.NotVerify ? "red" : "green",
                   DailyLimit = string.Format("{0:C}", verification.DailyLimit),
                   PersonDocuments = x.Person != null ? x.Person.PersonDocuments : null,
                   UserAddress = x.Person != null ? x.Person.PersonAddress : null,
                   IsVerifyMobile = x.Person != null ? x.Person.IsVerifyMobile.GetValueOrDefault() : false,
                   MobileCode = x.Person != null ? x.Person.MobileCode : string.Empty

               }).FirstOrDefault();

            return userProfile;
        }
        #endregion
    }
}