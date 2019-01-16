using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Talent.Data;
using Talent.Data.Entities;
using Talent.Service.Models;
using Talent.Core;
using Talent.Service.Domain;

namespace Talent.Service.Persons
{
    public class PersonService : IPersonService
    {
        private IRepository<Person> _personRepository;
        private IRepository<PersonAddress> _personAddressRepository;
        private IRepository<PersonDocument> _personDocumentRepository;

        public PersonService()
        {
        }

        public PersonService(IRepository<Person> personRepository,
                                IRepository<PersonAddress> personAddressRepository,
                                IRepository<PersonDocument> personDocumentRepository)
        {
            _personRepository = personRepository;
            _personAddressRepository = personAddressRepository;
            _personDocumentRepository = personDocumentRepository;
        }

        public List<Person> GetList()
        {
            var list = _personRepository.GetQueryable().Include("Login").ToList();
            return list;
        }

        public Verification GetVerification(int personId)
        {
            var person = _personRepository.GetById(personId);
            var personApprovedDocs = _personDocumentRepository.GetQueryable().Where(x => x.PersonId == personId && x.Status == (int)DocumentStatus.Approved);

            // TODO Institution, add a column in person table called IsInstitution

            if (personApprovedDocs.Any(x => x.DocumentType == (int)DocumentType.BankStatement) &&
                personApprovedDocs.Any(t => t.DocumentType == (int)DocumentType.IssuedID) &&
                personApprovedDocs.Any(n => n.DocumentType == (int)DocumentType.AddressProof) &&
                personApprovedDocs.Any(m => m.DocumentType == (int)DocumentType.SignedSelfie) &&
                person.IsVerifyMobile.GetValueOrDefault())
            {
                return new Verification
                {
                    LevelName = "Trusted",
                    Level = VerificationLevel.Trusted,
                    DailyLimit = 40000m
                };
            }

            if (personApprovedDocs.Any(x => x.DocumentType == (int)DocumentType.BankStatement) &&
                personApprovedDocs.Any(t => t.DocumentType == (int)DocumentType.IssuedID) &&
                personApprovedDocs.Any(m => m.DocumentType == (int)DocumentType.SignedSelfie) &&
                person.IsVerifyMobile.GetValueOrDefault())
            {
                return new Verification
                {
                    LevelName = "Verified",
                    Level = VerificationLevel.Verified,
                    DailyLimit = 8000m
                };
            }

            if (personApprovedDocs.Any(x => x.DocumentType == (int)DocumentType.BankStatement) &&
                personApprovedDocs.Any(t => t.DocumentType == (int)DocumentType.IssuedID))
            {
                return new Verification
                {
                    LevelName = "Newbie",
                    Level = VerificationLevel.Newbie,
                    DailyLimit = 500m
                };
            }

            return new Verification
            {
                LevelName = "Not Verified",
                Level = VerificationLevel.NotVerify,
                DailyLimit = 0
            };
        }

        public void UpdateProfile(UserProfileEditModel model)
        {
            var person = _personRepository.GetQueryable().FirstOrDefault(x => x.Id == model.Id);

            if (person != null)
            {
                person.FirstName = model.FirstName;
                person.MiddleName = model.MiddleName;
                person.LastName = model.LastName;
                person.MobilePhone = model.Mobile;
                person.CountryCode = model.CountryDialCode != null ? model.CountryDialCode.Split('-')[0] : string.Empty;
                person.DialCode = model.CountryDialCode != null ? model.CountryDialCode.Split('-')[1] : string.Empty;
                person.DateOfBirth = model.DOB;

                if (person.PersonAddress != null)
                {
                    person.PersonAddress.FlatNumber = model.FlatNumber;
                    person.PersonAddress.Street = model.Street;
                    person.PersonAddress.City = model.City;
                    person.PersonAddress.StateName = model.State;
                    person.PersonAddress.CountryName = model.Country;
                    person.PersonAddress.PostCode = model.PostCode;
                    person.PersonAddress.AdminNote = model.AdminNote;
                }
                else
                {
                    var address = new PersonAddress
                    {
                        FlatNumber = model.FlatNumber,
                        Street = model.Street,
                        City = model.City,
                        StateName = model.State,
                        CountryName = model.Country,
                        PostCode = model.PostCode,
                        AdminNote = model.AdminNote,
                        PersonId = model.Id,
                    };

                    _personAddressRepository.Add(address);
                }

                _personRepository.Update(person);
            }
        }

        /// <summary>
        /// Update the document status, then it will update user verification level
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="personId"></param>
        /// <param name="updatedBy"></param>
        public void UpdateDocumentStatus(int id, DocumentStatus status, int personId, int updatedBy)
        {
            var document = _personDocumentRepository.GetQueryable().FirstOrDefault(x => x.Id == id);

            if (document != null)
            {
                document.Status = (int)status;
                document.ModifiedDate = DateTime.UtcNow;
                document.ModifiedBy = updatedBy;

                _personDocumentRepository.Update(document);

                // Update the document owner's verification level
                var verification = GetVerification(personId);
                var person = _personRepository.GetById(personId);
                person.VerifiMarsionLevel = (int)verification.Level;
                _personRepository.Update(person);
            }
        }

        /// <summary>
        /// Update mobile code
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobileCode"></param>
        public void UpdateMobileCode(int id, string mobileCode)
        {
            var user = _personRepository.GetById(id);

            if (user != null && user.IsVerifyMobile.GetValueOrDefault() == false)
            {
                user.MobileCode = mobileCode;
                user.MobileCodeGenerated = DateTime.UtcNow;
                _personRepository.Update(user);
            }
        }


    }
}
