using Talent.Data;
using Talent.Data.Entities;
using Talent.Diagnostics;
using Talent.Service.Domain;
using Talent.Service.Models;
using Talent.Service.SendMail;
using Talent.WebApp.Models;
using System.Linq;
using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Talent.WebApp.Models.Profile;
using System.Web;
using System.IO;
using System.Collections.Generic;

namespace Talent.WebApp.Controllers
{
    public class HomeController : GeneralBaseController
    {
        private IRepository<Person> _personRepository;
        private IRepository<PersonLanguage> _personLanguageRepository;
        private IRepository<PersonDescription> _personDescriptionRespository;
        private IRepository<PersonAvailability> _personAvailability;
        private IRepository<PersonSkill> _personSkillRepository;
        private IRepository<PersonEducation> _personEducationRepository;
        private IRepository<PersonDocument> _personDocumentRepository;
        private IEmailService _emailService;
        private IApplicationContext _appContext;

        public HomeController(
            IRepository<Person> personRepository,
            IRepository<PersonLanguage> personLanguageRepository,
            IRepository<PersonDescription> personDescriptionRepository,
            IRepository<PersonAvailability> personAvailability,
            IRepository<PersonSkill> personSkillRepository,
            IRepository<PersonEducation> personEducation,
            IRepository<PersonDocument> personDocument,
            IEmailService emailService,
            IApplicationContext appContext) : base(appContext)
        {
            _personRepository = personRepository;
            _personLanguageRepository = personLanguageRepository;
            _personDescriptionRespository = personDescriptionRepository;
            _personAvailability = personAvailability;
            _personSkillRepository = personSkillRepository;
            _personEducationRepository = personEducation;
            _personDocumentRepository = personDocument;
            _emailService = emailService;
            _appContext = appContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ServiceListing()
        {
            return View();
        }

        public ActionResult IsUserAuthenticated()
        {
            if (_appContext.LoginId == 0)
            {
                return new JsonCamelCaseResult(new { IsAuthenticated = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var person = _personRepository.GetById(_appContext.LoginId);
                return new JsonCamelCaseResult(new { IsAuthenticated = true, Username = person.FirstName }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProfileDetail()
        {
            var loginDetail = _personRepository.GetById(_appContext.LoginId);
            return new JsonCamelCaseResult(new { Username = loginDetail.FirstName }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLanguages()
        {
            var languages = _personLanguageRepository.Get(x => x.PersonId == _appContext.LoginId);

            var jsonLanguageResult = languages.Select(x => new LanguagePersonListViewModel
            {
                PersonLanguageId = x.Id,
                Language = x.Language,
                LanguageLevel = x.LanguageLevel
            }).ToList();

            return new JsonCamelCaseResult(jsonLanguageResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddLanguage(AddLanguageViewModel language)
        {
            try
            {
                var newLanguage = new PersonLanguage
                {
                    PersonId = _appContext.LoginId,
                    LanguageLevel = language.Level,
                    Language = language.Name,
                };
                _personLanguageRepository.Add(newLanguage);
                return new JsonCamelCaseResult(new { Success = true, newLanguage.Id}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonCamelCaseResult(new { Success = false, Message = "Error while adding new language" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateLanguage(AddLanguageViewModel language)
        {
            var existingLanguage = _personLanguageRepository.GetById(language.Id);
            if (existingLanguage.PersonId != _appContext.LoginId)
            {
                return new JsonCamelCaseResult(new { Success = false, Message = "You are not allowed to edit this language" }, JsonRequestBehavior.AllowGet);
            }
            existingLanguage.Language = language.Name;
            existingLanguage.LanguageLevel = language.Level;
            _personLanguageRepository.Update(existingLanguage);
            return new JsonCamelCaseResult(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteLanguage(AddLanguageViewModel language)
        {
            var existingLanguage = _personLanguageRepository.GetById(language.Id);
            if (existingLanguage.PersonId != _appContext.LoginId)
            {
                return new JsonCamelCaseResult(new { Success = false, Message = "You are not allowed to delete this language" }, JsonRequestBehavior.AllowGet);
            }

            _personLanguageRepository.Delete(existingLanguage); 

            return new JsonCamelCaseResult(new { Success = true, existingLanguage.Language }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAvailabilityType()
        {
            var availabilityDetail = _personAvailability.Get(x => x.PersonId == _appContext.LoginId).FirstOrDefault();
            return new JsonCamelCaseResult(new
            {
                Availability = availabilityDetail?.AvailabilityType,
                EarnTarget = availabilityDetail?.EarnTarget,
                AvailableHours = availabilityDetail?.NumberOfAvailableHours
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSkills()
        {
            var skills = _personSkillRepository.Get(x => x.PersonId == _appContext.LoginId);

            var jsonSkillResult = skills.Select(x => new SkillPersonListViewModel
            {
                PersonSkillId = x.Id,
                Skill = x.Skill,
                ExperienceLevel = x.ExperienceLevel
            }).ToList();

            return new JsonCamelCaseResult(jsonSkillResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddSkill(AddSkillViewModel skill)
        {
            try
            {
                var newSkill = new PersonSkill
                {
                    PersonId = _appContext.LoginId,
                    Skill = skill.Name,
                    ExperienceLevel = skill.Level,
                    CreatedOn = DateTime.Now,
                    CreatedBy = _appContext.LoginId,
                    UpdatedOn = DateTime.Now,
                    UpdatedBy = _appContext.LoginId,
                    IsDeleted = false
                };
                _personSkillRepository.Add(newSkill);
                return new JsonCamelCaseResult(new { Success = true, newSkill.Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonCamelCaseResult(new { Success = false, Message = "Error while adding new skill " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateSkill(AddSkillViewModel skill)
        {
            var existingSkill = _personSkillRepository.GetById(skill.Id);
            if (existingSkill.PersonId != _appContext.LoginId)
            {
                return new JsonCamelCaseResult(new { Success = false, Message = "You are not allowed to edit this skill" }, JsonRequestBehavior.AllowGet);
            }
            existingSkill.Skill = skill.Name;
            existingSkill.ExperienceLevel = skill.Level;
            existingSkill.UpdatedOn = DateTime.Now;
            existingSkill.UpdatedBy = _appContext.LoginId;
            _personSkillRepository.Update(existingSkill);
            return new JsonCamelCaseResult(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            var contactModel = new ContactModel();
            var person = _personRepository.GetById(_appContext.LoginId);

            if (person != null)
            {
                contactModel.Email = person.Login != null ? person.Login.Username : string.Empty;
                contactModel.FirstName = person.FirstName;
                contactModel.LastName = person.LastName;
                contactModel.MobilePhone = person.MobilePhone;
            }

            contactModel.RecaptchaSiteKey = SiteUtil.RecaptchaSiteKey;
            return View(contactModel);
        }
        public ActionResult GetPersonAvailabilityDetails()
        {
            var availabilityDetail = _personAvailability.Get(x => x.PersonId == _appContext.LoginId).FirstOrDefault();
            return new JsonCamelCaseResult(new { AvailabilityType = availabilityDetail.AvailabilityType, EarnTarget = availabilityDetail.EarnTarget, AvailableHours = availabilityDetail.NumberOfAvailableHours }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddPersonEducation(AddEducationViewModel education)
        {
            try
            {
                var newEducation = new PersonEducation
                {
                    PersonId = _appContext.LoginId,
                    InstituteName = education.InstituteName,
                    Country = education.Country,
                    Title = education.Title,
                    Degree = education.Degree,
                    YearOfGraduation = education.YearOfGraduation,
                    CreatedOn = DateTime.Now,
                    CreatedBy = _appContext.LoginId,
                    UpdatedBy = _appContext.LoginId,
                    UpdatedOn = DateTime.Now,
                };

                _personEducationRepository.Add(newEducation);
                return new JsonCamelCaseResult(new { Success = true, newEducation.Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonCamelCaseResult(new { Success = false, Message = "Error while adding new language" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProfilePhoto()
        {
            var profilephoto = _personDocumentRepository.Get(x => x.PersonId == _appContext.LoginId && x.Status == 1 && x.DocumentType == 1).FirstOrDefault();
            string photoFileName = null;

            if (profilephoto != null)
                photoFileName = profilephoto.FileName;
          
            return new JsonCamelCaseResult(new { Success = true, photoFileName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProfilePhoto(HttpPostedFileBase imageFile)
        {
            var fs = Request.Files;
            var file = fs[0];
            var fileExtension = Path.GetExtension(file.FileName);
            List<string> acceptedExtensions = new List<string> { ".jpg", ".png", ".gif", ".jpeg" };

            if (fileExtension != null && !acceptedExtensions.Contains(fileExtension.ToLower()))
            {
                return new JsonCamelCaseResult(new { Success = false, Message = "Supported file types are *.jpg, *.png, *.gif, *.jpeg" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var fileName = Path.GetFileName(file.FileName);
                var index = fileName.LastIndexOf(".");
                var newFileName = fileName.Insert(0, $"{Guid.NewGuid()}");
                var physicalPath = Path.Combine(Server.MapPath("~/images"), newFileName);
                file.SaveAs(physicalPath);

                var ProfilePhoto = _personDocumentRepository.Get(x => x.PersonId == _appContext.LoginId && x.Status == 1 && x.DocumentType == 1).FirstOrDefault();

                try
                {

                    if (ProfilePhoto == null)
                    {

                        var newProfilePhoto = new PersonDocument
                        {
                            PersonId = _appContext.LoginId,
                            Status = 1,
                            FileName = newFileName,
                            CreatedDate = DateTime.Now,
                            CreatedBy = _appContext.LoginId,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = _appContext.LoginId,
                            DocumentType = 1
                        };

                        _personDocumentRepository.Add(newProfilePhoto);

                    }
                    else
                    {

                        ProfilePhoto.FileName = newFileName;
                        ProfilePhoto.ModifiedBy = _appContext.LoginId;
                        ProfilePhoto.ModifiedDate = DateTime.Now;

                        _personDocumentRepository.Update(ProfilePhoto);

                    }

                    return new JsonCamelCaseResult(new { Success = true, newProfilePhotoName = newFileName }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return new JsonCamelCaseResult(new { Success = false, Message = "Error while uploading Profile Photo" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpGet]
        public ActionResult GetPersonEducation()
        {
            var result = _personEducationRepository.Get(x => x.PersonId == _appContext.LoginId).Select(x => new
            {
                x.Id,
                x.InstituteName,
                x.Country,
                x.Title,
                x.Degree,
                x.YearOfGraduation
            }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditPersonEducation(AddEducationViewModel education)
        {
            var existingEducation = _personEducationRepository.GetById(education.Id);
            if (ModelState.IsValid && existingEducation != null)
            {
                if(existingEducation.PersonId != _appContext.LoginId)
                {
                    return new JsonCamelCaseResult(new { Success = false, Message = "You are not allowed to edit this education record" }, JsonRequestBehavior.AllowGet);
                }

                existingEducation.Country = education.Country;
                existingEducation.Degree = education.Degree;
                existingEducation.InstituteName = education.InstituteName;
                existingEducation.Title = education.Title;
                existingEducation.YearOfGraduation = education.YearOfGraduation;
                existingEducation.UpdatedOn = DateTime.Now;
                existingEducation.UpdatedBy = _appContext.LoginId;

                _personEducationRepository.Update(existingEducation);

                return new JsonCamelCaseResult(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            return new JsonCamelCaseResult(new { Success = false, Message = "There was an error in the recieved education data" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactModel model)
        {
            try
            {
                model.RecaptchaSiteKey = SiteUtil.RecaptchaSiteKey;

                if (!IsValidateReCaptcha())
                {
                    ModelState.AddModelError("", "Google reCaptcha validation failed.");
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    // Send user contact to support
                    // _emailService.SendContact(SiteUtil.SupportEmail, model);

                    ViewBag.Message = "Your contact has been sent.";
                }
                else
                {
                    ModelState.AddModelError("", "All fields are required.");
                    return View(model);
                }
            }
            catch (Exception x)
            {
                Logging.Error(x.Message, x);
                ModelState.AddModelError("", "Something went wrong, our team has been notified with this error.");
            }

            return View(model);
        }

        public ActionResult GetInfo()
        {
            var personInfo = _personDescriptionRespository.Get(x => x.PersonId == _appContext.LoginId).FirstOrDefault();
            string value = null;
            int id = 0;


            if (personInfo != null)
            {
                id = personInfo.Id;
                value = personInfo.Description;
            }

            return new JsonCamelCaseResult(new { Success = true, id, value  }, JsonRequestBehavior.AllowGet);
        }

         

        public ActionResult AddInfo(DescriptionViewModel pValue)
        {
         
            try
            {
                var newDescription = new PersonDescription
                {
                    PersonId = _appContext.LoginId,
                    Description = pValue.Description,
                };

                var existingDescription = _personDescriptionRespository.GetById(pValue.PersonId);

                if (existingDescription == null || existingDescription.Description == pValue.Description)
                { 

                    _personDescriptionRespository.Add(newDescription);
                }
                if (newDescription.Description == null)
                {
                    _personDescriptionRespository.Delete(existingDescription);
                }

                else
                {
                    existingDescription.PersonId = _appContext.LoginId;
                    existingDescription.Description = pValue.Description;
                    _personDescriptionRespository.Update(existingDescription);
                }
                return new JsonCamelCaseResult(new { Success = true,}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonCamelCaseResult(new { Success = false, Message = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ServiceDetail()
        {
            return View();
        }

        public ActionResult Search(string searchTerm)
        {
            return View();
        }

        public ActionResult NoAccess()
        {
            return View();
        }

        public ActionResult TOC()
        {
            return View();
        }

    }
}