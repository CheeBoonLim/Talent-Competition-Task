using Talent.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Talent.WebApp.Models.Account
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public Guid UID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string VerificationLevelNameCssColor { get; set; }
        public string VerificationLevelName { get; set; }
        public int VerificationLevel { get; set; }
        public string DailyLimit { get; set; }
        public string MobileCode { get; set; }
        public bool IsVerifyMobile { get; set; }
        public PersonAddress UserAddress { get; set; }
        public ICollection<PersonDocument> PersonDocuments { get; set; }
    }
}