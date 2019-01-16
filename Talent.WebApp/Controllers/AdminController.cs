using Talent.Core;
using Talent.Data;
using Talent.Data.Entities;
using Talent.Diagnostics;
using Talent.Service.Domain;
using Talent.Service.Models;
using Talent.Service.Persons;
using Talent.Service.SendMail;
using Talent.WebApp.Models;
using Talent.WebApp.Models.Admin;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Talent.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        IPersonService _personService;
        IRepository<PersonDocument> _personDocumentRepository;
        IApplicationContext _appContext;
        IEmailService _emailService;

        public AdminController(IPersonService customerService,
                                IRepository<PersonDocument> personDocumentRepository,
                                IApplicationContext applicationContext,
                                IEmailService emailService
                                )
        {
            _personService = customerService;
            _personDocumentRepository = personDocumentRepository;
            _appContext = applicationContext;
            _emailService = emailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Customer 

        public ActionResult Customer()
        {
            var list = _personService.GetList().OrderByDescending(n => n.CreatedDate);

            var models = list.Select(n => CustomerModel.Create(n, _appContext)).ToList();

            return View(models);
        }

        [HttpPost]
        public ActionResult UpdateDocumentStatus(int id, int status)
        {
            var document = _personDocumentRepository.GetById(id);

            if (document != null && document.Person != null && document.Person.Login != null)
            {
                // Keep an old status
                var oldStatus = Enum.GetName(typeof(DocumentStatus), document.Status);

                // Update document status
                _personService.UpdateDocumentStatus(id, (DocumentStatus)status, document.PersonId, _appContext.LoginId);

                // Send a notification
                var newStatus = Enum.GetName(typeof(DocumentStatus), status);
                var documentType = Enum.GetName(typeof(DocumentType), document.DocumentType);
                var email = document.Person.Login.Username;
                var customerFullName = string.Format("{0} {1}", document.Person.FirstName, document.Person.LastName);
                var changedBy = _appContext.UserName;

                // Send a notification to customer
                _emailService.SendChangedDocumentStatusNotification(SiteUtil.UploadDocumentURL, SiteUtil.SupportEmail, email,
                    oldStatus, newStatus, document.FileName, documentType, SiteUtil.SenderName, changedBy, customerFullName);

                // Send a notification to support
                _emailService.SendChangedDocumentStatusNotification(SiteUtil.UploadDocumentURL, changedBy, SiteUtil.SupportEmail,
                    oldStatus, newStatus, document.FileName, documentType, _appContext.FullName, changedBy, customerFullName);

                return Json(new { id = document.PersonId });
            }

            return Json(new { id = 0 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(UserProfileEditModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _personService.UpdateProfile(model);
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                    return Json(new { success = false, message = "The profile has been updated unsuccessfully." });
                }
            }
            catch (Exception x)
            {
                Logging.Error(x.Message, x);
                ModelState.AddModelError("", "Something went wrong, our team has been notified with this error.");
                return Json(new { success = false, message = "The profile has been updated unsuccessfully." });
            }

            return Json(new { success = true, message = "The profile has been updated successfully." });
        }

        #endregion
        
    }
}